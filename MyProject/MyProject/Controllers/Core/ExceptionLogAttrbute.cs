using MyProject.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Controllers.Core
{
    public class ExceptionLogAttrbute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            SysExceptionTask.AddException(filterContext.Exception, filterContext.HttpContext.Request.Url.ToString());
            filterContext.HttpContext.Response.Write("系统错误！");
            filterContext.HttpContext.Response.End();
        }
    }
}