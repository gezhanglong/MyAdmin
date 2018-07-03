using MyProject.Controllers.Core;
using MyProject.Task;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Areas.WeiXin.Controllers.WeiXinReceiveMessage
{
    public class WeiXinReceiveMessageController : Controller
    {
        private readonly WeiXinReceiveMessageTask _message = new WeiXinReceiveMessageTask();

        [SupportFilter]
        public ActionResult Index()
        {
            //var list = _message.GetList();
            //ViewBag.list = "{Rows:" + JsonConvert.SerializeObject(list) + ",Total:" + list.Count + "}";  //列表
            return View();
        }

    }
}
