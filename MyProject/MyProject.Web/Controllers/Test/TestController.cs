using MyProject.Services.Extensions;
using MyProject.Services.Utility;
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

        public ActionResult Testcase()
        {
            var result1 = new DateTime(636728598000000000).AddHours(8);
            var result2 = DateTime.Now.Ticks;
                return Content("");
        }

    }
}
