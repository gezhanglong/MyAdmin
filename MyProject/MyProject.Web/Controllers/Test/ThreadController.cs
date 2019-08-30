using MyProject.Core.Entities;
using MyProject.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Web.Controllers.Test
{
    //线程学习
    public class ThreadController : Controller
    {
        

        public ActionResult Index()
        {
            for(var i=1;i<=4;i++)
            {
                Thread thread1 = new Thread(new ParameterizedThreadStart(ThreadTest_1));
                //Thread thread2 = new Thread(new ParameterizedThreadStart(ThreadTest_1));
                thread1.Start(i*5);
                //thread2.Start(20);
            } 
            return Content("正在执行...");
        }

        public void ThreadTest_1(object num)
        {
            var _logtask = new LogTask();
            var number = (int)num;
            for(var i=0;i< number; i++)
            {
                var log = new Log()
                {
                    CreateTime = DateTime.Now,
                    Msg = num+"No:" + i,
                    Ret = 89,
                };
                _logtask.AddLog(log);
                Thread.Sleep(1000*number);
            }
            
        }

    }
}
