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

        protected void Application_End(object sender, EventArgs e)
        {
            #region 可解决IIS应用程序池自动回收的问题 
            var log = new LogTask();
            try
            {
                log.AddLog(new Core.Entities.Log() { CreateTime = DateTime.Now, Msg = "OK,Job is workend", Ret = 0 });
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
        
        /// <summary>
        /// 创建一个定时器
        /// </summary>
        public void CreateTime()
        {
            var log = new LogTask();
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
            time.Interval = 1000 * 60 * 5;
            time.Start();
        }

        /// <summary>
        /// 创建一个作业
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void CreateJob(object sender, ElapsedEventArgs args)
        {
            var log = new LogTask();
            try
            { 
                log.AddLog(new Core.Entities.Log() { CreateTime = DateTime.Now, Msg = "OK,Job is working", Ret = 0 });
            }
            catch (Exception e)
            {
                log.AddLog(new Core.Entities.Log() { CreateTime = DateTime.Now, Msg = "No,"+e.Message, Ret = 0 });
            }
          
        }
        
    }


}