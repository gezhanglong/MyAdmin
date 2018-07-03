using MyProject.Matrix.Controllers.Core;
using MyProject.Task;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Matrix.Controllers.WeiXinReceiveMessage
{
    public class WeiXinReceiveMessageController : BaseController
    {
        private readonly WeiXinReceiveMessageTask _message = new WeiXinReceiveMessageTask();

        [SupportFilter]
        public ActionResult Index(string fields, string sdate, string edate,string ContentValue,bool IsLike=true, string fieldValue="", int pageIndex = 1, int pageSize = 10)
        {
            var fieldList = new List<SelectListItem>();
            fieldList.Insert(0, new SelectListItem { Text = "全部", Value = "" });
            fieldList.Insert(1, new SelectListItem { Text = "开发者微信号", Value = "ToUserName" });
            fieldList.Insert(2, new SelectListItem { Text = "发送方帐号", Value = "FromUserName" });
            fieldList.Insert(3, new SelectListItem { Text = "接收消息类型", Value = "MsgType" }); 
            ViewData["fieldsList"] = fieldList;
            ViewData["fieldValue"] = fieldValue;
            ViewData["ContentValue"] = ContentValue;
            ViewData["sdate"] = sdate;
            ViewData["edate"] = edate;
            var list = _message.GetPagedList(fields, fieldValue,sdate,edate,ContentValue,IsLike,pageIndex, pageSize); 
            return View(list);
        }

    }
}
