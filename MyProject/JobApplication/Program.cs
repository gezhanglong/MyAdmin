﻿using MyProject.Core.Entities;
using MyProject.Task;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JobApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            try { 
                var properties = new NameValueCollection();
                properties["quartz.scheduler.instanceName"] = "我的作业1";

                // 设置线程池
                properties["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
                properties["quartz.threadPool.threadCount"] = "5";
                properties["quartz.threadPool.threadPriority"] = "Normal";

                // //===远程输出配置=== 
                properties["quartz.scheduler.exporter.type"] = "Quartz.Simpl.RemotingSchedulerExporter, Quartz";
                properties["quartz.scheduler.exporter.port"] = "556";
                properties["quartz.scheduler.exporter.bindName"] = "QuartzScheduler";
                properties["quartz.scheduler.exporter.channelType"] = "tcp";

                //===持久化====
                //存储类型
                properties["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz";
                //表明前缀
                properties["quartz.jobStore.tablePrefix"] = "QRTZ_";
                //驱动类型
                properties["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.SqlServerDelegate, Quartz";
                //数据源名称
                properties["quartz.jobStore.dataSource"] = "MyP";
                //连接字符串
                properties["quartz.dataSource.MyP.connectionString"] = @"Server=.\SQLEXPRESS;Database=MyProject;Uid=sa;Pwd=123456;";
                //properties["quartz.dataSource.MyP.connectionString"] = @"Server=.;Database=MyProject;Uid=sa;Pwd=123456;";
                //sqlserver版本
                properties["quartz.dataSource.MyP.provider"] = "SqlServer-20";

                //===集群===
                properties["quartz.jobStore.clustered"] = "true";
                properties["quartz.scheduler.instanceId"] = "AUTO";

                // First we must get a reference to a scheduler
                ISchedulerFactory sf = new StdSchedulerFactory(properties);
                IScheduler sched = sf.GetScheduler();


                //IJobDetail job = JobBuilder.Create<YouJob>()
                //  .WithIdentity("job6", "6")
                //  .Build();

                ////什么时候开始执行
                //DateTime runTime = Convert.ToDateTime("2018-9-29 13:35:00");
                //ITrigger trigger = TriggerBuilder.Create()
                //.WithIdentity("trigger6", "6")
                //.StartAt(runTime)
                //    //.WithCronSchedule("0 0/5 16 * * ? *")
                //    .WithSimpleSchedule(x => x
                //    .WithIntervalInSeconds(60) //1秒一次真男人
                //    .WithRepeatCount(5))//循环5次
                //    // .RepeatForever())//无限循环
                //.Build();

                //sched.ScheduleJob(job, trigger);


                //启动任务
                Console.WriteLine("Start:" + DateTime.Now);
                //var trigger = new MyTriggerListener();
                //trigger.Name="trigger";
                //sched.ListenerManager.AddTriggerListener(trigger, KeyMatcher<TriggerKey>.KeyEquals(new TriggerKey("trigger2", "group1"))); 
                sched.Start(); 
            }
            catch (Exception e) {
                Console.WriteLine("errormsg:" + e);
                Console.Read();
            }
          
           
        }
    }

    public class IpProxyJob : IJob
    { 
        public void Execute(IJobExecutionContext context)
        {
            
        }

    }
    public class YouJob : IJob
    { 
        public void Execute(IJobExecutionContext context)
        { 
            Console.WriteLine("Youtime:" + DateTime.Now);
        }

    }
     
    public class MyJob2 : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            System.Threading.Thread.Sleep(1000 * 60 * 10);//挂起10分钟
            Console.WriteLine("time2:" + DateTime.Now);
        }

    }

    //触发器监听
    public class MyTriggerListener : ITriggerListener
    {
        private string name;

        public void TriggerComplete(ITrigger trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode)
        {
            Console.WriteLine("job完成时调用");
        }
        public void TriggerFired(ITrigger trigger, IJobExecutionContext context)
        {
            Console.WriteLine("job执行时调用");
        }
        public void TriggerMisfired(ITrigger trigger)
        {

            Console.WriteLine("错过触发时调用(例：线程不够用的情况下)" + trigger.Description);
        }
        public bool VetoJobExecution(ITrigger trigger, IJobExecutionContext context)
        {
            //Trigger触发后，job执行时调用本方法。true即否决，job后面不执行。
            return false;
        }
        public string Name { get { return name; } set { name = value; } }
    }
}
