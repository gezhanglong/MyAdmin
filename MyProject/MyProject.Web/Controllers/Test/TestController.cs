using MyProject.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Web.Controllers.Test
{
    public class TestController : Controller
    { 

        public ActionResult TestNo1()
        {
            var num = DateTimeExtensions.GetMillisecondByDateTime(Convert.ToDateTime("2018-8-29 11:05:00"));
            return Content(num.ToString());
        }

    }
}
