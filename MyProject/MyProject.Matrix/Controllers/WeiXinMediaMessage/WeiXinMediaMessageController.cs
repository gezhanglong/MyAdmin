using MyProject.Controllers;
using MyProject.Core.Enums;
using MyProject.Task;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyProject.Services.Extensions;
using Deepleo.Weixin.SDK;
using System.IO;
using MyProject.Services.Utility;
using Codeplex.Data;
using System.Text;
using MyProject.Matrix.Controllers.Core;
using System.Net;

namespace MyProject.Matrix.Controllers.WeiXinMediaMessage
{
    public class WeiXinMediaMessageController : BaseController
    {
        private readonly WeiXinMediaMessageTask _message = new WeiXinMediaMessageTask();
        private readonly WeiXinSdkTask _sdk = new WeiXinSdkTask();

        [SupportFilter]
        public ActionResult Index(int pageIndex=1, int pageSize=15)
        {
            ViewBag.perm = GetPermission();
            var list = _message.GetPagedList(pageIndex,pageSize); 
            return View(list);
        }
        [SupportFilter]
        public ActionResult Save()
        {
            ViewData["MediaList"] = WeiXinMediaMessageEnum.image.ToSelectList();
            ViewData["MediaTypeList"] = WeiXinMediaTypeMessageEnum.Forever.ToSelectList();
            return View();
        }

        [HttpPost]
        public ActionResult Save(HttpPostedFileBase filebase)
        {

            var mediaTitle = Request.Form["MediaTitle"];
            var mediaType = Request.Form["MediaType"];
            var isForever = Request.Form["IsForever"];
            var url = "https://api.weixin.qq.com/cgi-bin/media/upload";//临时素材
            var type = "";
            if (isForever == "0")
            {
                url = "https://api.weixin.qq.com/cgi-bin/material/add_material";//永久素材
            }
            switch (mediaType)
            {
                case "1": type = WeiXinMediaMessageEnum.image.ToString(); break;
                case "2": type = WeiXinMediaMessageEnum.voice.ToString(); break;
                case "3": type = WeiXinMediaMessageEnum.video.ToString(); break;
                case "4": type = WeiXinMediaMessageEnum.thumb.ToString(); break;
            }
            HttpPostedFileBase file = Request.Files["files"];
            string filename = Path.GetFileName(file.FileName);
            var bytes = new byte[file.InputStream.Length];
            file.InputStream.Read(bytes, 0, (int)file.InputStream.Length);
             
            var fileItem = new FileItem(filename, bytes,"image/jpeg");
            var fileParas = new Dictionary<string, FileItem>{
            {"file",fileItem}
            };
            var textParas = new Dictionary<string, string>{
                {"access_token",_sdk.AccountToken()},//
                {"type",type}
             };
            var result = JsonConvert.DeserializeObject<MediaResult>(WebUtils.DoPost(url, textParas, fileParas));

            if (result == null || result.errcode != null)
            {
                return CloseParentBox("上传失败errcode:" + result.errcode + ";errmsg:" + result.errmsg, "");
            }
            var info = new MyProject.Core.Entities.WeiXinMediaMessage
            {
                Creater = GetCurrentAdmin(),
                CreateTime = DateTime.Now,
                IsForever = Convert.ToInt32(isForever),
                MediaTitle = mediaTitle,
                MediaType = type,
                MediaId = result.media_id,
                Url = result.url
            };
            _message.Add(info);
            return CloseParentBox("操作成功", "/WeiXinMediaMessage/index");

        }



       

        [SupportFilter]
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            var media = _message.GetById(Convert.ToInt32(id));
            var result=new MediaResult();
            if(media !=null)
            {
                if (media.IsForever == 1)
                {
                    if (media.CreateTime.AddDays(3) < DateTime.Now)
                    {
                        _message.DeleteById(Convert.ToInt32(id)); 
                    }
                    else
                    {
                        result.errcode ="-1";
                        result.errmsg = "临时素材无删除操作,请过期后再删除本地数据库";
                    }
                    
                    return Json(result);
                }
                var textParas = new Dictionary<string, string>{ 
                {"media_id",media.MediaId},
                {"access_token",_sdk.AccountToken()}
             };
                result = JsonConvert.DeserializeObject<MediaResult>(WebUtils.DoPost("https://api.weixin.qq.com/cgi-bin/material/del_material?access_token=" + _sdk.AccountToken(), "{\"media_id\":\"" + media.MediaId+"\"}"));
                 if (result.errcode == "0")
                 {
                     _message.DeleteById(Convert.ToInt32(id));
                 } 
            }
            return Json(result);
        }

    }

    public class MediaResult
    {
        public string type{get;set; }
        public string media_id{get;set; }
        public string created_at{get;set; }
        public string errcode{get;set; }
        public string errmsg{get;set; }
        public string url{get;set; }
    }
 
}
