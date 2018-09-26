using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobNo2Application
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var properties = new NameValueCollection();
                properties["quartz.scheduler.instanceName"] = "我的作业55";

                // 设置线程池
                properties["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
                properties["quartz.threadPool.threadCount"] = "5";
                properties["quartz.threadPool.threadPriority"] = "Normal";

                // //===远程输出配置=== 
                properties["quartz.scheduler.exporter.type"] = "Quartz.Simpl.RemotingSchedulerExporter, Quartz";
                properties["quartz.scheduler.exporter.port"] = "557";
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


                //IJobDetail job = JobBuilder.Create<MyJob66>()
                //  .WithIdentity("job77", "group55")
                //  .Build();

                ////什么时候开始执行
                //DateTime runTime = Convert.ToDateTime("2018-9-18 17:30:00");
                //ITrigger trigger = TriggerBuilder.Create()
                //.WithIdentity("trigger77", "group55")
                //.StartAt(runTime)
                //.WithSimpleSchedule(x => x
                //.WithIntervalInSeconds(60) //1秒一次真男人
                //.RepeatForever())//无限循环
                //.Build();

                //sched.ScheduleJob(job, trigger);


                //启动任务
                Console.WriteLine("Start55:" + DateTime.Now); 
                sched.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine("errormsg:" + e);
                Console.Read();
            }


        }
    }

    public class MyJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("time55:" + DateTime.Now);
        }

    }

    public class MyJob66 : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("time66:" + DateTime.Now);
        }

    }

    public class MyJob88 : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("time88:" + DateTime.Now);
        }

    }
}
