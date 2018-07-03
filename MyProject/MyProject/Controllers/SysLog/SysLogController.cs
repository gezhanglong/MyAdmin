using MyProject.Controllers.Core;
using MyProject.Task;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Controllers.SysLog
{
    public class SysLogController : Controller
    {
        private readonly SysLogTask _task = new SysLogTask();

        [SupportFilter]
        public ActionResult Index()
        {
            //var list = _task.GetList();
            //ViewBag.list = "{Rows:" + JsonConvert.SerializeObject(list) + ",Total:" + list.Count + "}";  //列表
            return View();
        }

    }
}
