using MyProject.Controllers.Core;
using MyProject.Task;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Controllers.SysException
{
    public class SysExceptionController : Controller
    {
        private readonly SysExceptionTask _task = new SysExceptionTask();

        [SupportFilter]
        public ActionResult Index()
        {
            //var list = _task.GetList();
            //ViewBag.list = "{Rows:" + JsonConvert.SerializeObject(list) + ",Total:" + list.Count + "}";  //列表
            return View();
        }

    }
}
