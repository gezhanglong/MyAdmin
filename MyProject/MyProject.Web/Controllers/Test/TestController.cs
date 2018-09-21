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
            var result1 = "";
            var result2 = "";
            var result3 = "";
            result1 = WebUtils.DoPost("https://apiqa.lkgame.com/superlottery/dosharew?UnionId=oxC3qvz5VNrOms3Qbhv7nmKYeMu0&roundcode=20180918011", "");
            result1 = result1 + DateTime.Now.ToString();
            result2 = WebUtils.DoPost("https://apiqa.lkgame.com/superlottery/dosharew?UnionId=oxC3qvz5VNrOms3Qbhv7nmKYeMu0&roundcode=20180918011", "");
            result2 = result2 + DateTime.Now.ToString();
            result3 = WebUtils.DoPost("https://apiqa.lkgame.com/superlottery/dosharew?UnionId=oxC3qvz5VNrOms3Qbhv7nmKYeMu0&roundcode=20180918011", "");
            result3 = result3 + DateTime.Now.ToString();



                return Content(result1 + result2 + result3);
        }

    }
}
