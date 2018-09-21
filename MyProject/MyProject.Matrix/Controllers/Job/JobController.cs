using MyProject.Core.Entities;
using MyProject.Task; 
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Matrix.Controllers.Job
{
    public class JobController : Controller
    {
        private static readonly LogTask log = new LogTask(); 

        public ActionResult Index()
        {
            //var properties = new NameValueCollection();
            //properties["quartz.scheduler.instanceName"] = "我的作业";

            //// 设置线程池
            //properties["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
            //properties["quartz.threadPool.threadCount"] = "5";
            //properties["quartz.threadPool.threadPriority"] = "Normal";

            //// 远程输出配置
            //properties["quartz.scheduler.exporter.type"] = "Quartz.Simpl.RemotingSchedulerExporter, Quartz";
            //properties["quartz.scheduler.exporter.port"] = "556";
            //properties["quartz.scheduler.exporter.bindName"] = "QuartzScheduler";
            //properties["quartz.scheduler.exporter.channelType"] = "tcp";



            //log.AddLog(new Log() { CreateTime = DateTime.Now, Msg = "job,start:", Ret = 0 });
            //// First we must get a reference to a scheduler
            //ISchedulerFactory sf = new StdSchedulerFactory(properties);
            //IScheduler sched = sf.GetScheduler();
            //IJobDetail job = JobBuilder.Create<MyJob>()
            //  .WithIdentity("job1", "group21")
            //  .Build();

            ////什么时候开始执行
            //DateTime runTime = Convert.ToDateTime("2018-9-18 17:30:00");
            //ITrigger trigger = TriggerBuilder.Create()
            //.WithIdentity("trigger1", "group1")
            //.StartAt(runTime)
            //.WithSimpleSchedule(x => x
            //.WithIntervalInSeconds(60) //1秒一次真男人
            //.RepeatForever())//无限循环
            //.Build();

            //sched.ScheduleJob(job, trigger);
            ////启动任务
            //sched.Start();
            return Content("OK");
        }

    }
    //public class MyJob : IJob
    //{
    //    private readonly LogTask log = new LogTask();

    //    public void Execute(IJobExecutionContext context)
    //    {
    //        log.AddLog(new Log() { CreateTime = DateTime.Now, Msg = "job,start:" + DateTime.Now, Ret = 0 });
    //    }

    //}
}
