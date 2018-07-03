using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Services.Utility
{
    public class CacheHelper
    {
        /// <summary>
        /// 获取key缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(string key)
        {
            return MemoryCache.Default.Get(key);
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public static void ClearCache()
        {
            var caches = MemoryCache.Default.AsParallel().ToList();
            foreach (var item in caches)
            {
                MemoryCache.Default.Remove(item.Key);
            }
        }

        /// <summary>
        /// 清空key缓存
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(string key)
        {
            MemoryCache.Default.Remove(key);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="cacheSeconds">秒</param>
        public static void Set(string key, object value, int cacheSeconds)
        {
            MemoryCache.Default.Set(key, value, DateTime.Now.AddSeconds(cacheSeconds));
        }
    }
}
