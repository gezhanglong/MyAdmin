using MyProject.Services.Extensions;
using MyProject.Task;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Timers;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MyProject.Matrix
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
       
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            CreateTime();//执行作业
        }


        private readonly JobControllTask _jobTask = new JobControllTask();
        private readonly LogTask log = new LogTask();

        protected void Application_End(object sender, EventArgs e)
        {
            #region 可解决IIS应用程序池自动回收的问题  
            try
            {
                log.AddLog(new Core.Entities.Log() { CreateTime = DateTime.Now, Msg = "OK,Job is workend", Ret = 0 });
                _jobTask.UpdateAllIsTimeing();
            }
            catch (Exception ex)
            {
                log.AddLog(new Core.Entities.Log() { CreateTime = DateTime.Now, Msg = "No,end" + ex.Message, Ret = 0 });
            }

            string url = "http://localhost:8088/Home/Index";
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            Stream receiveStream = myHttpWebResponse.GetResponseStream();//得到回写的字节流   
            #endregion
        } 

      
        // 创建一个定时器 
        public void CreateTime()
        { 
            try
            {
                log.AddLog(new Core.Entities.Log() { CreateTime = DateTime.Now, Msg = "Job is start", Ret = 0 });
            }
            catch (Exception ex)
            {
                log.AddLog(new Core.Entities.Log() { CreateTime = DateTime.Now, Msg = "No,start" + ex.Message, Ret = 0 });
            }
            var time = new Timer();
            time.Elapsed += CreateJob;
            time.Interval = 1000 * 60 * 1;
            time.Start(); 
        }


        Timer time1 = new Timer();
        Timer time2 = new Timer();
        // 创建一个主作业 
        public void CreateJob(object sender, ElapsedEventArgs args)
        { 
            try
            {
                var jobList = _jobTask.GetAll();
                foreach (var item in jobList)
                {
                    switch (item.JobName)
                    {
                        case "JobNo1": 
                            if (item.JobNumAlready == 1 && !item.IsTimeing)
                            {
                                time1.Stop();
                                time2.Elapsed += CreateJobNo1;
                                time2.Interval = item.JobSpace;
                                time2.Start();
                                item.IsTimeing = true;  //更新 运行状态为开启
                                _jobTask.UpdateJob(item);
                                log.AddLog(new Core.Entities.Log() { CreateTime = DateTime.Now, Msg = "循环作业JobNo1开启 " + item.JobSpace, Ret = 0 });
                            }
                            if (item.JobNumAlready >= item.JobNum && item.IsTimeing)
                            {
                                time2.Stop();
                                item.IsOpen = false;    //报错关闭作业
                                item.IsTimeing = false;
                                _jobTask.UpdateJob(item);
                                log.AddLog(new Core.Entities.Log() { CreateTime = DateTime.Now, Msg = "作业结束 ", Ret = 0 });
                            }
                            log.AddLog(new Core.Entities.Log() { CreateTime = DateTime.Now, Msg = "轮询JobNo1 ", Ret = 0 });


                            if (item.IsOpen)//是否开启作业
                            {
                                if (DateTime.Now >= item.StartTime && DateTime.Now < item.EndTime)//作业的时间范围
                                {
                                    if (item.JobNum == 0 || item.JobNum - item.JobNumAlready > 0)//作业执行次数 JobNum=0是无限循环
                                    {
                                        try
                                        {
                                            if (!item.IsTimeing)//运行状态为停止
                                            {
                                                time1.Elapsed += CreateJobNo1;
                                                time1.Interval = DateTimeExtensions.GetMillisecondByDateTime(item.JobTime);
                                                time1.Start();


                                                item.IsTimeing = true;  //更新 运行状态为开启
                                                _jobTask.UpdateJob(item);
                                                log.AddLog(new Core.Entities.Log() { CreateTime = DateTime.Now, Msg = "作业开始JobNo1： " + DateTimeExtensions.GetMillisecondByDateTime(item.JobTime), Ret = 0 });
                                            }
                                             
                                            break;
                                        }
                                        catch (Exception e)
                                        {
                                            item.JobError = e.Message;
                                            item.IsOpen = false;    //报错关闭作业
                                            _jobTask.UpdateJob(item);
                                        }
                                        break;
                                    }
                                }
                                item.IsOpen = false;//超过时间范围或者次数已经执行完 就关闭作业
                                _jobTask.UpdateJob(item);
                            }
                            break;
                        case "JobNo2":

                            break;
                        case "JobNo3":

                            break;

                    }
                }
                
            }
            catch (Exception e)
            { 
                log.AddLog(new Core.Entities.Log() { CreateTime = DateTime.Now, Msg = "No,"+e.Message, Ret = 0 });
            }
          
        }

        //作业JobNo1
        public void CreateJobNo1(object sender, ElapsedEventArgs args)
        {  
            log.AddLog(new Core.Entities.Log() { CreateTime = DateTime.Now, Msg = "OK,JobNo1 is working start", Ret = 0 });
            System.Threading.Thread.Sleep(1000*60*10);//挂起10分钟
            log.AddLog(new Core.Entities.Log() { CreateTime = DateTime.Now, Msg = "OK,JobNo1 is working end", Ret = 0 });
            UpdateJonByJobName("JobNo1");
        }



        
        // 更新 已经执行次数 
        public void UpdateJonByJobName(string jobName)
        {
            var job = _jobTask.GetByJobName(jobName); 
            job.JobNumAlready++;
            if (job.JobNumAlready == 1)
            {
                job.IsTimeing = false;  //更新 运行状态为关闭 
            }
            job.JobTime = DateTime.Now.AddMilliseconds(job.JobSpace);
            _jobTask.UpdateJob(job);
        }
        
    }


}