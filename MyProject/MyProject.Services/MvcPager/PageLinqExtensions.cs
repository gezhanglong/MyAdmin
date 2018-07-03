

using System.Linq;
using System.Collections.Generic;

namespace MyProject.Services.MvcPager
{
    public static class PageLinqExtensions
    {
        public static PagedList<T> ToPagedList<T>
            (
                this IQueryable<T> allItems,
                int pageIndex,
                int pageSize
            )
        {
            if (pageIndex < 1)
                pageIndex = 1;
            var itemIndex = (pageIndex - 1) * pageSize;
            var pageOfItems = allItems.Skip(itemIndex).Take(pageSize);
            var totalItemCount = allItems.Count();
            return new PagedList<T>(pageOfItems, pageIndex, pageSize, totalItemCount);
        }

        public static PagedList<T> ToPagedList<T>
            (
                this IEnumerable<T> allItems,
                int pageIndex,
                int pageSize
            )
        {
            if (pageIndex < 1)
                pageIndex = 1;
            var itemIndex = (pageIndex - 1) * pageSize;
            var pageOfItems = allItems.Skip(itemIndex).Take(pageSize);
            var totalItemCount = allItems.Count();
            return new PagedList<T>(pageOfItems, pageIndex, pageSize, totalItemCount);
        }

        public static PagedList<T> ToPagedList<T>
            (
                this IQueryable<T> allItems,
                int pageIndex,
                int pageSize,
                int totalCount
            )
        {
            if (pageIndex < 1)
                pageIndex = 1;
            var totalItemCount = totalCount;
            return new PagedList<T>(allItems, pageIndex, pageSize, totalItemCount);
        }

        public static PagedList<T> ToPagedList<T>
            (
                this IEnumerable<T> allItems,
                int pageIndex,
                int pageSize,
                int totalCount
            )
        {
            if (pageIndex < 1)
                pageIndex = 1;
            var totalItemCount = totalCount;
            return new PagedList<T>(allItems, pageIndex, pageSize, totalItemCount);
        }
    }
}
