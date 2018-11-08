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

        public ActionResult TestNo1(string host, int post)
        {
            string str = WebUtils.DoGetProxy("http://www.baidu.com",host,post);
            return Content(str);
        }

        public ActionResult Testcase()
        {
            var result1 = new DateTime(636728598000000000).AddHours(8);
            var result2 = DateTime.Now.Ticks;
                return Content("");
        }

    }
}
