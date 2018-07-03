using MyProject.Task;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyProject.Core.Entities;
using MyProject.Areas.WeiXin.Models;
using MyProject.Services.Mappers;
using MyProject.Core.Enums;
using MyProject.Services.Extensions;
using MyProject.Controllers;
using MyProject.Controllers.Core;

namespace MyProject.Areas.WeiXin.Controllers.WeiXinReplyMessage
{
    public class WeiXinReplyMessageController : BaseController
    {
        private readonly WeiXinReplyMessageTask _message = new WeiXinReplyMessageTask();
        private readonly WeiXinMediaMessageTask _mediaMessage = new WeiXinMediaMessageTask();

        [SupportFilter]
        public ActionResult Index()
        {
            //var list = _message.GetList();
            //ViewBag.list = "{Rows:" + JsonConvert.SerializeObject(list) + ",Total:" + list.Count + "}";  //列表
            return View();
        }

        [SupportFilter]
        public ActionResult Save(int? id)
        {
            #region 初始化
            var model = new WeiXinReplyMessageModel();
            //ViewData["MsgTypeList"] = WeiXinMessageTypeEnum.image.ToSelectList();
            //ViewData["ThumbMediaIdList"] = WeiXinMessageTypeEnum.image.ToSelectList();
            //var madiaList = _mediaMessage.GetList().ToSelectList(c => c.MediaId, c => c.MediaType + "-" + c.MediaTitle);
            //madiaList.Insert(0, new SelectListItem { Text = "请选择", Value = string.Empty });
            //ViewData["MediaIdList"] = madiaList;
            //var picurlList = _mediaMessage.GetList().Where(c => c.MediaType == "image" && c.Url!=null).ToSelectList(c => c.Url, c => c.MediaType + "-" + c.MediaTitle);
            //picurlList.Insert(0, new SelectListItem { Text = "请选择", Value = string.Empty });
            //ViewData["picurlList"] = picurlList;
            model.MsgType = "1";
            #endregion
            if (id !=null)
            { 
                var info = _message.GetById((int)id);
                if (info != null)
                {
                    model = EntityMapper.Map<MyProject.Core.Entities.WeiXinReplyMessage, WeiXinReplyMessageModel>(info);
                    #region 图文信息特殊处理
                    if (model.MsgType == "6")
                    {
                        var titleStr = model.Title.Split(';');
                        var descriptionStr = model.Description.Split(';');
                        var picUrlStr = model.PicUrl.Split(';');
                        var urlStr = model.Url.Split(';');
                        model.Title = titleStr[0];
                        model.Title1 = titleStr[1];
                        model.Title2 = titleStr[2];
                        model.Title3 = titleStr[3];
                        model.Title4 = titleStr[4];
                        model.Title5 = titleStr[5];
                        model.Title6 = titleStr[6];
                        model.Title7 = titleStr[7];

                        model.Description = descriptionStr[0];
                        model.Description1 = descriptionStr[1];
                        model.Description2 = descriptionStr[2];
                        model.Description3 = descriptionStr[3];
                        model.Description4 = descriptionStr[4];
                        model.Description5 = descriptionStr[5];
                        model.Description6 = descriptionStr[6];
                        model.Description7 = descriptionStr[7];

                        model.PicUrl = picUrlStr[0];
                        model.PicUrl1 = picUrlStr[1];
                        model.PicUrl2 = picUrlStr[2];
                        model.PicUrl3 = picUrlStr[3];
                        model.PicUrl4 = picUrlStr[4];
                        model.PicUrl5 = picUrlStr[5];
                        model.PicUrl6 = picUrlStr[6];
                        model.PicUrl7 = picUrlStr[7];

                        model.Url = urlStr[0];
                        model.Url1 = urlStr[1];
                        model.Url2 = urlStr[2];
                        model.Url3 = urlStr[3];
                        model.Url4 = urlStr[4];
                        model.Url5 = urlStr[5];
                        model.Url6 = urlStr[6];
                        model.Url7 = urlStr[7];
                    } 
                    #endregion
                } 
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Save(WeiXinReplyMessageModel model)
        { 
            if (ModelState.IsValid)
            {
                #region 图文信息特殊处理 
                    model.Title = model.Title+";"+model.Title1+";"+model.Title2+";"+model.Title3+";"+model.Title4+";"+model.Title5+";"+model.Title6+";"+model.Title6+model.Title7;
                    model.Description = model.Description+";"+model.Description1+";"+model.Description2+";"+model.Description3+";"+model.Description4+";"+model.Description5+";"+model.Description6+";"+model.Description7;
                    model.PicUrl = model.PicUrl + ";" + model.PicUrl1 + ";" + model.PicUrl2 + ";" + model.PicUrl3 + ";" + model.PicUrl4 + ";" + model.PicUrl5 + ";" + model.PicUrl6 + ";" + model.PicUrl7;
                    model.Url = model.Url + ";" + model.Url1 + ";" + model.Url2 + ";" + model.Url3 + ";" + model.Url4 + ";" + model.Url5 + ";" + model.Url6 + ";" + model.Url7; 
                #endregion
                if (model.Id == null)
                {
                    var info = new MyProject.Core.Entities.WeiXinReplyMessage
                    {
                        Creater=GetCurrentAdmin().UserName,
                        CreateTime = DateTime.Now, 
                        MatchKey=model.MatchKey,
                        MsgType=model.MsgType,
                        Content=model.Content,
                        MediaId=model.MediaId,
                        Title=model.Title,
                        Description=model.Description,
                        MusicURL=model.MusicURL,
                        HQMusicUrl=model.HQMusicUrl,
                        ThumbMediaId=model.ThumbMediaId,
                        ArticleCount=model.ArticleCount,
                        Articles=model.Articles,
                        PicUrl=model.PicUrl,
                        Url=model.Url
                    };
                    _message.Add(info);
                }
                else
                {
                    var info = _message.GetById((int)model.Id);
                    if (info != null)
                    {
                        info.Creater = GetCurrentAdmin().UserName;
                        info.MatchKey=model.MatchKey;
                        info.MsgType=model.MsgType;
                        info.Content=model.Content;
                        info.MediaId=model.MediaId;
                        info.Title=model.Title;
                        info.Description=model.Description;
                        info.MusicURL=model.MusicURL;
                        info.HQMusicUrl=model.HQMusicUrl;
                        info.ThumbMediaId=model.ThumbMediaId;
                        info.ArticleCount=model.ArticleCount;
                        info.Articles=model.Articles;
                        info.PicUrl=model.PicUrl;
                        info.Url = model.Url;
                        _message.Update(info);
                    }
                }
                return CloseParentBox("操作成功", "/weixin/WeiXinReplyMessage/index");
            }

            #region 初始化
            //ViewData["MsgTypeList"] = WeiXinMessageTypeEnum.image.ToSelectList();
            //ViewData["ThumbMediaIdList"] = WeiXinMessageTypeEnum.image.ToSelectList();
            //var madiaList = _mediaMessage.GetList().ToSelectList(c => c.MediaId, c => c.MediaType + "-" + c.MediaTitle);
            //madiaList.Insert(0, new SelectListItem { Text = "请选择", Value = string.Empty });
            //ViewData["MediaIdList"] = madiaList;
            //var picurlList = _mediaMessage.GetList().Where(c => c.MediaType == "image").ToSelectList(c => c.Url, c => c.MediaType + "-" + c.MediaTitle);
            //picurlList.Insert(0, new SelectListItem { Text = "请选择", Value = string.Empty });
            //ViewData["picurlList"] = picurlList;   
            #endregion

            return View(model);
        }

        [SupportFilter]
        [HttpPost]
        public void Del(int? id)
        {
            _message.DeleteById(Convert.ToInt32(id)); 
        }

    }
}
