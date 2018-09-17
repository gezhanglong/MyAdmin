using Newtonsoft.Json;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Services.Utility
{
    public class RedisHelper
    {
        private string _path;
        private string _redisPath;
        private static Dictionary<string, PooledRedisClientManager> prcms = new Dictionary<string, PooledRedisClientManager>();

        public RedisHelper(string path)
        {
            _path = path;
            _redisPath = ConfigurationSettings.AppSettings[_path];
            if (!prcms.ContainsKey(path))
            {
                prcms.Add(path, CreateManager(new string[] { _redisPath }, new string[] { _redisPath }));
            }
        }

        private PooledRedisClientManager CreateManager(string[] readWriteHosts, string[] readOnlyHosts)
        {
            return new PooledRedisClientManager(readWriteHosts, readOnlyHosts,
                new RedisClientManagerConfig { MaxWritePoolSize = 1000, MaxReadPoolSize = 1000, AutoStart = true, });
        }

        /// <summary>
        ///  redis 广播
        /// </summary>
        /// <param name="channel">广播频道</param>
        /// <param name="data">数据</param>
        public bool Publish(string channel, string data, out string error)
        {
            error = string.Empty;
            try
            {
                using (IRedisClient redis = prcms[_path].GetClient())
                {
                    redis.PublishMessage(channel, data);
                    return true;
                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return false;
            }
        }

        #region -- Item --
        /// <summary>
        /// 设置单体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public bool Item_Set<T>(string key, T t)
        {
            try
            {
                using (IRedisClient redis = prcms[_path].GetClient())
                {
                    return redis.Set<T>(key, t);
                }
            }
            catch (Exception ex)
            {
                // LogInfo
            }
            return false;
        }

        /// <summary>
        /// 获取单体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Item_Get<T>(string key) where T : class
        {
            using (IRedisClient redis = prcms[_path].GetClient())
            {
                return redis.Get<T>(key);
            }
        }

        /// <summary>
        /// 移除单体
        /// </summary>
        /// <param name="key"></param>
        public bool Item_Remove(string key)
        {
            using (IRedisClient redis = prcms[_path].GetClient())
            {
                return redis.Remove(key);
            }
        }

        #endregion

        #region -- List --

        public void List_Add<T>(string key, T t)
        {
            using (IRedisClient redis = prcms[_path].GetClient())
            {
                var redisTypedClient = redis.GetTypedClient<T>();
                redisTypedClient.AddItemToList(redisTypedClient.Lists[key], t);
            }
        }



        public bool List_Remove<T>(string key, T t)
        {
            using (IRedisClient redis = prcms[_path].GetClient())
            {
                var redisTypedClient = redis.GetTypedClient<T>();
                return redisTypedClient.RemoveItemFromList(redisTypedClient.Lists[key], t) > 0;
            }
        }
        public void List_RemoveAll<T>(string key)
        {
            using (IRedisClient redis = prcms[_path].GetClient())
            {
                var redisTypedClient = redis.GetTypedClient<T>();
                redisTypedClient.Lists[key].RemoveAll();
            }
        }

        public long List_Count(string key)
        {
            using (IRedisClient redis = prcms[_path].GetClient())
            {
                return redis.GetListCount(key);
            }
        }

        public List<T> List_GetRange<T>(string key, int start, int count)
        {
            using (IRedisClient redis = prcms[_path].GetClient())
            {
                var c = redis.GetTypedClient<T>();
                return c.Lists[key].GetRange(start, start + count - 1);
            }
        }


        public List<T> List_GetList<T>(string key)
        {
            using (IRedisClient redis = prcms[_path].GetClient())
            {
                var c = redis.GetTypedClient<T>();
                return c.Lists[key].GetRange(0, c.Lists[key].Count);
            }
        }

        public List<T> List_GetList<T>(string key, int pageIndex, int pageSize)
        {
            int start = pageSize * (pageIndex - 1);
            return List_GetRange<T>(key, start, pageSize);
        }

        /// <summary>
        /// 设置缓存过期
        /// </summary>
        /// <param name="key"></param>
        /// <param name="datetime"></param>
        public void List_SetExpire(string key, DateTime datetime)
        {
            using (IRedisClient redis = prcms[_path].GetClient())
            {
                redis.ExpireEntryAt(key, datetime);
            }
        }
        #endregion

        #region -- Set --
        public void Set_Add<T>(string key, T t)
        {
            using (IRedisClient redis = prcms[_path].GetClient())
            {
                var redisTypedClient = redis.GetTypedClient<T>();
                redisTypedClient.Sets[key].Add(t);
            }
        }
        public bool Set_Contains<T>(string key, T t)
        {
            using (IRedisClient redis = prcms[_path].GetClient())
            {
                var redisTypedClient = redis.GetTypedClient<T>();
                return redisTypedClient.Sets[key].Contains(t);
            }
        }
        public bool Set_Remove<T>(string key, T t)
        {
            using (IRedisClient redis = prcms[_path].GetClient())
            {
                var redisTypedClient = redis.GetTypedClient<T>();
                return redisTypedClient.Sets[key].Remove(t);
            }
        }
        #endregion

        #region -- Hash --
        /// <summary>
        /// 判断某个数据是否已经被缓存


        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public bool Hash_Exist<T>(string key, string dataKey)
        {
            using (IRedisClient redis = prcms[_path].GetClient())
            {
                return redis.HashContainsEntry(key, dataKey);
            }
        }

        /// <summary>
        /// 存储数据到hash表


        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public bool Hash_Set<T>(string key, string dataKey, T t)
        {
            using (IRedisClient redis = prcms[_path].GetClient())
            {

                string value = JsonConvert.SerializeObject(t);
                return redis.SetEntryInHash(key, dataKey, value);
            }
        }
        /// <summary>
        /// 移除hash中的某值


        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public bool Hash_Remove(string key, string dataKey)
        {
            using (IRedisClient redis = prcms[_path].GetClient())
            {
                return redis.RemoveEntryFromHash(key, dataKey);
            }
        }
        /// <summary>
        /// 移除整个hash
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public bool Hash_Remove(string key)
        {
            using (IRedisClient redis = prcms[_path].GetClient())
            {
                return redis.Remove(key);
            }
        }
        /// <summary>
        /// 从hash表获取数据


        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        public T Hash_Get<T>(string key, string dataKey)
        {
            using (IRedisClient redis = prcms[_path].GetClient())
            {
                string value = redis.GetValueFromHash(key, dataKey);
                return JsonConvert.DeserializeObject<T>(value);

            }
        }
        /// <summary>
        /// 获取整个hash的数据


        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<T> Hash_GetAll<T>(string key)
        {
            using (IRedisClient redis = prcms[_path].GetClient())
            {
                var list = redis.GetHashValues(key);
                if (list != null && list.Count > 0)
                {
                    List<T> result = new List<T>();
                    foreach (var item in list)
                    {
                        var value = JsonConvert.DeserializeObject<T>(item);
                        result.Add(value);
                    }
                    return result;
                }
                return null;
            }
        }
        /// <summary>
        /// 设置缓存过期
        /// </summary>
        /// <param name="key"></param>
        /// <param name="datetime"></param>
        public void Hash_SetExpire(string key, DateTime datetime)
        {
            using (IRedisClient redis = prcms[_path].GetClient())
            {
                redis.ExpireEntryAt(key, datetime);
            }
        }

        public int Hash_GetCount(string key)
        {
            try
            {
                using (IRedisClient redis = prcms[_path].GetClient())
                {
                    return int.Parse(redis.GetHashCount(key).ToString());
                }
            }
            catch
            {
                return 0;
            }
        }


        #endregion

        #region -- SortedSet --

        /// <summary>
        /// 添加到有序集
        /// </summary>
        public bool SortedSet_Add<T>(string key, T t, double score)
        {
            using (IRedisClient redis = prcms[_path].GetClient())
            {
                string value = JsonConvert.SerializeObject(t);
                return redis.AddItemToSortedSet(key, value, score);
            }
        }

        /// <summary>
        /// 获取有序集合的值


        /// </summary>
        public double SortedSet_Get<T>(string key, T t)
        {
            using (IRedisClient redis = prcms[_path].GetClient())
            {
                string value = JsonConvert.SerializeObject(t);
                return redis.GetItemScoreInSortedSet(key, value);
            }
        }

        /// <summary>
        /// 删除有序集结点


        /// </summary>
        public bool SortedSet_Remove<T>(string key, T t)
        {
            using (IRedisClient redis = prcms[_path].GetClient())
            {
                string value = JsonConvert.SerializeObject(t);
                return redis.RemoveItemFromSortedSet(key, value);
            }
        }

        /// <summary>
        ///  获取键值的降序排名
        /// </summary>
        public long SortedSet_GetItemIndexDesc<T>(string key, T t)
        {
            using (IRedisClient redis = prcms[_path].GetClient())
            {
                string value = JsonConvert.SerializeObject(t);
                return redis.GetItemIndexInSortedSetDesc(key, value) + 1;
            }
        }

        /// <summary>
        /// 获取降序排名列表
        /// </summary>
        public IDictionary<string, double> GetRangeWithScoresFromSortedSetDesc(string key, int fromRank, int toRank)
        {
            using (IRedisClient redis = prcms[_path].GetClient())
            {
                return redis.GetRangeWithScoresFromSortedSetDesc(key, fromRank, toRank);
            }
        }

        /// <summary>
        /// 设置缓存过期
        /// </summary>
        /// <param name="key"></param>
        /// <param name="datetime"></param>
        public void SortedSet_SetExpire(string key, DateTime datetime)
        {
            using (IRedisClient redis = prcms[_path].GetClient())
            {
                redis.ExpireEntryAt(key, datetime);
            }
        }
        #endregion
    }
}
