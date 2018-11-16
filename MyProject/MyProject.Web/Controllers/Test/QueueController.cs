using MyProject.Core.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Web.Controllers.Test
{
    public class QueueController : Controller
    {
        private readonly RequestResultDto _result = new RequestResultDto() { Ret = -1, Msg = "" };
        private static Queue q = new Queue(5);
        /// <summary>
        /// 队列
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int userid)
        {
            if(q.Count>=5)
            {
                _result.Msg = "队列已满";
                return Json(_result);
            }
            q.Enqueue(userid);
            return View();
        }

    }
}
