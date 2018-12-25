using MyProject.Core.Dtos;
using MyProject.Services.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
        public ActionResult Index(string  userid)
        {
            #region 统计执行时间
            //Stopwatch wc = new Stopwatch();//
            //wc.Start();
            //var id = CacheHelper.Get("mycache") as string;
            //wc.Stop();
            //var time1 = wc.ElapsedMilliseconds;
            //wc.Reset();
            //if (string.IsNullOrEmpty(id))
            //{
            //    wc.Start();
            //    CacheHelper.Set("mycache", userid, 100);
            //    wc.Stop();
            //}
            //return Content(id + ";" + time1 + ";" + wc.ElapsedMilliseconds); 
            #endregion


            if (q.Count>=5)
            {
                _result.Msg = "队列已满";
                return Json(_result);
            }
            q.Enqueue(userid);
            return View();
        }

    }
}
