using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DotnetSpider.Core;
using DotnetSpider.Core.Pipeline;
using DotnetSpider.Core.Processor;
using DotnetSpider.Core.Scheduler;
using DotnetSpider.Core.Selector;
using DotnetSpider.Extension;
using DotnetSpider.Core.Downloader;
using DotnetSpider.Extension.Model;
using DotnetSpider.Extension.Model.Attribute;
using DotnetSpider.Extension.Pipeline;
using MyProject.Core.Entities;
using MyProject.Task;
using DotnetSpider.Core.Proxy;

namespace spiderApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var run =new  CustmizeProcessorAndPipeline();
            run.DoRun();
            Console.WriteLine("Press any key to continue...");
            Console.Read();
        }


       
    }
    public class CustmizeProcessorAndPipeline
    {
        public void DoRun()
        {
            // Config encoding, header, cookie, proxy etc... 定义采集的 Site 对象, 设置 Header、Cookie、代理等
            var site = new Site { EncodingName = "UTF-8", RemoveOutboundLinks = true, Domains = "www.xicidaili.com".Split(';')  };
            //for (int i = 1; i < 5; ++i)
            //{
            // Add start/feed urls. 添加初始采集链接
            //site.AddStartUrl($"http://www.xicidaili.com/nn/1");

            string[] urls = { "http://www.xicidaili.com/nn/1"
                             ,"http://www.xicidaili.com/nn/2"
                             ,"http://www.xicidaili.com/nn/3"
                             ,"http://www.xicidaili.com/nn/4"
                             ,"http://www.xicidaili.com/nn/5"};
            site.AddStartUrls(urls);
            //}
            Spider spider = Spider.Create(site,
                // use memoery queue scheduler. 使用内存调度
                new QueueDuplicateRemovedScheduler(),
                // use custmize processor for youku 为优酷自定义的 Processor
                new YoukuPageProcessor())
                // use custmize pipeline for youku 为优酷自定义的 Pipeline
                .AddPipeline(new YoukuPipeline());
            spider.Downloader = new HttpClientDownloader();
            spider.ThreadNum = 1;
            spider.EmptySleepTime = 3000;
             //spider.Deep = 4;
            // 启动爬虫
            spider.Run(); 
        }

    }

    public class YoukuPipeline : BasePipeline
    { 
        public override void Process(IEnumerable<ResultItems> resultItems, ISpider spider)
        { 
            var _ipProxyTask = new IpProxyTask();
            //var count = _ipProxyTask.GetPagedList(1, 100).Count; 
            //if(count>=100)
            //{
            //    spider.Exit();
            //    return;
            //}
            var num = 0;
            try
            {
                
                foreach (var resultItem in resultItems)
                {
                    foreach (IpProxy entry in resultItem.Results["IpProxyResult"])
                    {
                        var result = _ipProxyTask.AddIpProxy(entry);
                        if(result.Ret==0)
                        {
                            num++; 
                        }
                    }
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("ok:"+num);
        }
    }

    public class YoukuPageProcessor : BasePageProcessor
    {
        protected override void Handle(Page page)
        {
            // 利用 Selectable 查询并构造自己想要的数据对象 
            var totalVideoElements = page.Selectable.SelectList(Selectors.XPath("//tr/td[2] | //tr/td[3] | //tr/td[4] | //tr/td[5] | //tr/td[6]")).Nodes().ToList();
            List<IpProxy> results = new List<IpProxy>();
            var i = 1;
            var j = 0;
            var name = "";
            foreach (var videoElement in totalVideoElements)
            {

                
                if (i%5==0)
                { 
                    var proxy = new IpProxy();
                    var strs = name.Split(':');
                    proxy.CreateTime = DateTime.Now;
                    proxy.FlushTime = DateTime.Now;
                    proxy.Host = strs[1];
                    proxy.Port = strs[2];
                    proxy.Serve = strs[3];
                    proxy.IsHide = strs[4];
                    proxy.HttpType= videoElement.GetValue();
                    results.Add(proxy);
                    name = "";
                    j++;
                }else
                {
                    if(i%(3+j*5)==0)
                    {
                        name = name + ":" + videoElement.XPath("a").GetValue();
                    }
                    else
                    {
                        name = name + ":" + videoElement.GetValue();
                    }
                    
                }
                
                i++;
            } 

            //  以自定义KEY存入page对象中供Pipeline调用
            page.AddResultItem("IpProxyResult", results);

            //  解析需要采集的URL
            foreach (var url in page.Selectable.SelectList(Selectors.XPath("//*[@id='body']/div[2]/a[position()<3]")).Links().Nodes())
            {
                page.AddTargetRequest(new Request(url.GetValue(), null));
            }
        }
    }

    

}
