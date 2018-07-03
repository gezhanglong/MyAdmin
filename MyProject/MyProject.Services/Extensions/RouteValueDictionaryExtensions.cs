#region using

using System;
using System.Reflection;
using System.Web.Routing;

#endregion

namespace MyProject.Services.Extensions
{
    public static class RouteValueDictionaryExtensions
    {
        /// <summary>
        ///   加载路由规则
        /// </summary>
        /// <param name = "routes">路由</param>
        /// <param name = "controller">controller</param>
        /// <param name = "action">Action</param>
        /// <param name = "routeValue">附加的参数</param>
        /// <returns></returns>
        public static RouteValueDictionary Load(this RouteValueDictionary routes, string action, string controller,
                                                params object[] routeValue)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes");
            }

            if (action == null)
            {
                throw new ArgumentException("action");
            }
            if (!string.IsNullOrEmpty(controller))
            {
                routes.Add("controller", controller);
            }
            routes.Add("action", action);
            foreach (var obj in routeValue)
            {
                FilterProperties(obj, routes);
            }
            return routes;
        }

        /// <summary>
        ///   附加属性-值到路由数组
        /// </summary>
        /// <param name = "obj"></param>
        /// <param name = "properties"></param>
        public static void FilterProperties(object obj, RouteValueDictionary properties)
        {
            var type = obj.GetType();
            if (type != null)
            {
                PropertyInfo[] publicProperties =
                    type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

                foreach (PropertyInfo pi in publicProperties)
                {
                    if (pi.GetIndexParameters().Length > 0)
                    {
                        continue;
                    }

                    Type currentPropertyType = pi.PropertyType;
                    Type currentUnderlyingType = Nullable.GetUnderlyingType(currentPropertyType);

                    if (currentUnderlyingType != null)
                    {
                        currentPropertyType = currentUnderlyingType;
                    }

                    if ((currentPropertyType.IsPrimitive || currentPropertyType.Equals(typeof (string)) ||
                         currentPropertyType.Equals(typeof (DateTime)) || currentPropertyType.Equals(typeof (decimal)) ||
                         currentPropertyType.Equals(typeof (Guid)) ||
                         currentPropertyType.Equals(typeof (DateTimeOffset)) ||
                         currentPropertyType.Equals(typeof (TimeSpan))) && pi.CanRead)
                    {
                        if (!properties.ContainsKey(pi.Name))
                        {
                            properties.Add(pi.Name, pi.GetValue(obj, null));
                        }
                    }
                    else if (currentPropertyType.IsEnum)
                    {
                        if (!properties.ContainsKey(pi.Name))
                        {
                            properties.Add(pi.Name, (int) pi.GetValue(obj, null));
                        }
                    }
                }
            }
        }
    }
}