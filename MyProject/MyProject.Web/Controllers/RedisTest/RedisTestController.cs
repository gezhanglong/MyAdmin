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

        public ActionResult Index()
        {
            var redis = new RedisHelper("MyRedis");
            //redis.Item_Set<string>("my", "yyyyyy");
            var value = redis.Item_Get<string>("my");
            //redis.SortedSet_Add<int>("smy", 34, 3400);
            //redis.SortedSet_Add<int>("smy", 3, 3);
            //redis.SortedSet_Add<int>("smy", 340, 340);
            var desc = redis.GetRangeWithScoresFromSortedSetDesc("smy", 0,10); 
            return Content(value);
            
        }

    }
}
