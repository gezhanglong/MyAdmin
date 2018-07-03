using MyProject.Core.Enum;
using MyProject.Matrix.Controllers.Core;
using MyProject.Task;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyProject.Services.Extensions;

namespace MyProject.Matrix.Controllers.SysLog
{
    public class SysLogController : BaseController
    {
        private readonly SysLogTask _logTask = new SysLogTask();
        private readonly SysExceptionTask _extTask = new SysExceptionTask();

        [SupportFilter]
        public ActionResult IndexLog(string edate, string sdate, int logType = 0, int logModule = 0, int pageIndex = 1, int pageSize = 15)
        {

            var logtype = LogTypeEnum.Add.ToSelectList();
            logtype.Insert(0, new SelectListItem { Text="全部",Value="0"});
            ViewData["LogTypeList"] = logtype;
            var logModul = LogModuleEnum.Land.ToSelectList();
            logModul.Insert(0, new SelectListItem { Text = "全部", Value = "0" });
            ViewData["LogModuleList"] = logModul;
            ViewData["sdate"] = sdate;
            ViewData["edate"] = edate;
            var model = _logTask.GetPagedList(edate,sdate,logType,logModule, pageIndex, pageSize);
            return View(model);
        }
        [HttpPost]
        public ActionResult GetLog(int id)
        {
            return Json(_logTask.GetSysLog(id));
        }

       

        [SupportFilter]
        public ActionResult IndexExt(string edate, string sdate, string helpLink, int pageIndex = 1, int pageSize = 15)
        {
            var model = _extTask.GetPagedList(edate,sdate,helpLink, pageIndex, pageSize);
            return View(model);
        }

        [HttpPost]
        public ActionResult GetExt(int id)
        {
            return Json(_extTask.GetExt(id));
        }

    }
}
