using System;

namespace MyProject.Services.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 时间转long类型
        /// </summary>
        /// <param name="now"></param>
        /// <returns></returns>
        public static long GenerateTimeStamp(this DateTime now)
        {
            TimeSpan ts = now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds) + Environment.TickCount;
        }



        /// <summary>
        /// 时间戳转DateTime  timestamp为10位秒级* 10000000，若为13位毫秒级*10000。
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime TimestampToDateTime(long timestamp)
        {

            DateTime dateTimeStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); 
            long lTime = timestamp * 10000000; 
            TimeSpan nowTimeSpan = new TimeSpan(lTime); 
            DateTime resultDateTime = dateTimeStart.Add(nowTimeSpan); 
            return resultDateTime;

        }
 

        /// <summary>
        /// 传入日期计算当前时间距离该日期还有多少毫秒
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static double GetMillisecondByDateTime(DateTime time)
        {
            if (time <= DateTime.Now)
            {
                return 0;
            }
            var times = time - DateTime.Now;
            return  times.TotalMilliseconds;
        }

        /// <summary>
        /// 计算两个时间差多少毫秒
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static double GetMillisecondByDateTime(DateTime sTime,DateTime eTime)
        {
            if (eTime <= sTime)
            {
                return 0;
            }
            var times = eTime - sTime;
            return times.TotalMilliseconds;
        }
    }
}