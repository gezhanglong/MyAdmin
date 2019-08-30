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
    //代理请求
    public class ProxyController : Controller
    {

        private readonly IpProxyTask _ipProxyTask = new IpProxyTask();
        private const int ipnum= 100;     //每个线程处理多少个ip
        private const int threadnum = 5; //最多 多少个线程

        private List<string> _urlList = new List<string> { 
            "https://jingyan.baidu.com/article/2a1383287c4e33474b134f13.html",// php开启curl模块
            "https://jingyan.baidu.com/article/a948d651aebb504a2ccd2e22.html",// 内网穿透之ngrok
            "https://jingyan.baidu.com/article/d3b74d64909eb05f76e60927.html",// mysql-8.0.17 安
        };

        public ActionResult Index()
        { 
            var list =  _ipProxyTask.GetPagedList(1, ipnum*threadnum);
            for (var i=0;i<list.Count/ ipnum; i++)
            {
                Thread threadProxy = new Thread(new ParameterizedThreadStart(GoProxy));//开(list.Count/ipnum)个线程跑代理请求
                threadProxy.Start(list.GetRange(i* ipnum, ipnum)); 
            } 

            var otherNum = list.Count - ((list.Count / ipnum) * ipnum);
            if(otherNum>0)
            {
                Thread threadOtherProxy = new Thread(new ParameterizedThreadStart(GoProxy));//开1个线程跑剩余的代理请求
                threadOtherProxy.Start(list.GetRange(((list.Count / ipnum) * ipnum), otherNum));
            }
           

            return Content("正在执行"+ ((list.Count / ipnum) + (otherNum > 0?1:0))+ "个线程...");
        }


        //执行代理请求
        public void GoProxy(object obj)
        {
            var list =(List<IpProxy>)obj;
            var errornum = 0;
            var oknum = 0;
            var newtime = DateTime.Now;
            foreach (var item in list)
            {
                var res = Services.Utility.WebUtils.DoGetProxy("http://baidu.com", item.Host, Convert.ToInt32(item.Port));
                if (res != "OK")
                {
                    _ipProxyTask.DelIpProxy(item.Host);
                }
                else
                {
                    item.Serve = item.Serve + res;
                    _ipProxyTask.UpdateIpProxy(item);
                    foreach (var url in _urlList)
                    {
                        res = Services.Utility.WebUtils.DoGetProxy(url, item.Host, Convert.ToInt32(item.Port));
                        if (res != "OK")
                        {
                            errornum++;
                        }
                        else
                        {
                            oknum++;
                        }
                    }
                }

            }
            var _logtask = new LogTask();
            var log = new Log()
            {
                CreateTime = DateTime.Now,
                Msg = "newtime:" + newtime + ";oknum:" + oknum + ";errornum:" + errornum,
                Ret = 88,
            };
            _logtask.AddLog(log);
        }


        public ActionResult IndexTest()
        {
            
            var list = _ipProxyTask.GetPagedList(1,10);

            foreach (var item in list)
            {
                try
                {
                    var res = Services.Utility.WebUtils.HttpGet("https://jingyan.baidu.com/article/d3b74d64909eb05f76e60927.html", item.Host, Convert.ToInt32(item.Port));
                }
                catch(Exception e)
                {
                    var er = e.Message;
                }
            }

                return Content("正在执行...");
        }

        public ActionResult IndexTestweb()
        {
            var res = Services.Utility.WebUtils.HttpGet("https://jingyan.baidu.com/article/2a1383287c4e33474b134f13.html");
            //var list = _ipProxyTask.GetPagedList(1, 5);

            //GoProxy(list);

            return Content(res);
        }
    }
}
