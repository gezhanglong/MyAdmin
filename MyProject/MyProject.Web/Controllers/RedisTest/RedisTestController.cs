using MyProject.Services.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Web.Controllers.RedisTest
{
    public class RedisTestController : Controller
    { 

        public ActionResult Index(string rankname,int  userid,double score=1)
        {
            var redis = new RedisHelper("MyRedis");
            //redis.Item_Set<string>("my", "yyyyyy");
            //var value = redis.Item_Get<string>("my");
            redis.SortedSet_Add<int>(rankname, userid, score); 
            var desc = redis.GetRangeWithScoresFromSortedSetDesc(rankname, 0,10);
            var str="";
            foreach(var item in desc)
            {
                str += "userid:" + item.Key + ";score:" + item.Value + "\n";
            }
            str += "    SortedSet_Get:" + redis.SortedSet_Get(rankname, userid);
            str += "    SortedSet_GetItemIndexDesc:" + redis.SortedSet_GetItemIndexDesc(rankname, userid);
            return Content(str);
            
        }

    }
}
