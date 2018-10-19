using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DotnetSpider.Core;
using DotnetSpider.Core.Pipeline;
using DotnetSpider.Core.Processor;
using DotnetSpider.Extraction;
using System.Security.Policy;
using DotnetSpider.Core.Scheduler;
using DotnetSpider.Downloader;

namespace spiderApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var site = new Site { CycleRetryTimes = 3, SleepTime = 300 };
            var spider = Spider.Create(site, new GithubProfileProcessor()).AddStartUrl("https://github.com/zlzforever");
            spider.ThreadNum = 5;
            spider.Run();
            Console.Read();
            Console.WriteLine("Press any key to continue...");
            Console.Read();
        }
    }

    public class MyPageProcessor : BasePageProcessor
    {
        protected override void Handle(Page page)
        {
            // 利用 Selectable 查询并构造自己想要的数据对象   
            var totalVideoElements = page.Selectable().SelectList(Selectors.XPath("//div[@class='yk-pack pack-film']")).Nodes();
            List<DataModel> results = new List<DataModel>();
            foreach (var videoElement in totalVideoElements)
            {
                var video = new DataModel();
                video.Name = videoElement.Select(Selectors.XPath(".//img[@class='quic']/@alt")).GetValue();
                results.Add(video);
            }

            // Save data object by key. 以自定义KEY存入page对象中供Pipeline调用
            page.AddResultItem("VideoResult", results);
        }
         
    }

    public class MyPipeline : BasePipeline
    {
        private static long count = 0; 

        public override void Process(IList<ResultItems> resultItems,  dynamic sender = null)
        {
            StringBuilder builder = new StringBuilder();
            foreach (ResultItems entry in resultItems)
            {
                count++;
                builder.Append($" [YoukuVideo {count}] {entry.Values}");
            }
            Console.WriteLine(builder);

            // Other actions like save data to DB. 可以自由实现插入数据库或保存到文件
        }
    }
}
