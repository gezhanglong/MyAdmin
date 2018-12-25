using MyProject.Services.Extensions;
using MyProject.Services.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Web.Controllers.Test
{
    public class TestController : Controller
    {

        public ActionResult TestNo1(string host, int post)
        { 
            Thread task = new Thread(new ParameterizedThreadStart(A_FallEvent));
            return Content("");
        }
        public void A_FallEvent(object str)
        {
             str = "冲出A";
        }
        public ActionResult Testcase()
        {
            var res = DateTimeExtensions.TimestampToDateTime(1445866601);
            var time = DateTimeExtensions.GenerateTimeStamp(new DateTime());
            var result1 = new DateTime(636728598000000000).AddHours(8);
            var result2 = DateTime.Now.Ticks;
            return Content("");
        }

        public ActionResult TestDelegate()
        {
            var my = new My("S");
            MyDelegate del;
            var del1 = new MyDelegate(my.MyMethod);
            var del2 = new MyDelegate(my.YouMethod);
            del = del1;
            del += del2;

            return Content(del("BBBBB"));
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public delegate string MyDelegate(string str);
    public class My
    {
        private string _MyStr;
        public My(string str)
        {
            _MyStr = str;
        }
        public string MyMethod(string str)
        {
            return _MyStr + str + "My";
        }
        public string YouMethod(string str)
        {
            return _MyStr + str + "You";
        }
    }

    public delegate string RaiseEventHandler(string hand);
    public delegate string  FallEventHandler ();
    public class MyeventA
    {
        public event FallEventHandler FallEvent;
        public event RaiseEventHandler RaiseEvent;

        public string  Raise(string hand)
        {
            if(RaiseEvent !=null)
            {
                return  RaiseEvent(hand);
            }
            return "";
        } 

        public string  Fall()
        {
            if(FallEvent != null)
            {
                return FallEvent();
            }
            return "";
        }
    }

    public class MyeventB
    {
        MyeventA a;
        public MyeventB(MyeventA a)
        {
            a.RaiseEvent += new RaiseEventHandler(A_RaiseEvent);
            a.FallEvent += new FallEventHandler(A_FallEvent);
        }
        public virtual string A_RaiseEvent(string hand)
        {
            if(hand=="right")
            {
                return "冲出A";
            }
            return "";
        }

        public virtual string  A_FallEvent()
        { 
                return "冲出A"; 
        }
    }

    //public class MyeventC:MyeventB
    //{
    //    //public MyeventC(MyeventA a)
    //    //{

    //    //}
    //}
}
