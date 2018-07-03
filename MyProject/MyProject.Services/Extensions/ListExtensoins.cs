using System.Collections.Generic;
using System.Linq;

namespace MyProject.Services.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class EnumerableExtensoins
    {
        public static IEnumerable<T> Add<T>(this IEnumerable<T> list,T item)
        {
            list.ToList().Add(item);
            return list;
        }
    }
}
