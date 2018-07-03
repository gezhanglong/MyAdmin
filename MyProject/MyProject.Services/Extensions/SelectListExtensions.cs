using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;

namespace MyProject.Services.Extensions
{
    public static class SelectListExtensions
    {
        public static List<SelectListItem> ToSelectList<TEnum>(this TEnum enumValue)
        {
            var list =
                Enum.GetValues(typeof(TEnum)).Cast<TEnum>()
                    .Select(e => new SelectListItem { Value = Convert.ToInt32(e).ToString(), Text = e.ToDescriptionName() })
                    .ToList();
            return list;
        }

        public static string ToDescriptionName<TEnum>(this TEnum enumValue)
        {
            try
            {
                var attributes = enumValue.GetType().GetField(enumValue.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attributes.Length > 0)
                {
                    var attr = (DescriptionAttribute)attributes[0];
                    return attr.Description;
                }
            }
            catch
            {


            }
            return enumValue.ToString();
        }

        public static List<SelectListItem> ToSelectList<T>(this IEnumerable<T> itemsToMap, Func<T, string> valueProperty, Func<T, string> textProperty)
        {
            if (itemsToMap == null || itemsToMap.Count() == 0)
                return new List<SelectListItem>();

            return itemsToMap.Select(item => new SelectListItem
                                                 {
                                                     Text = textProperty(item),
                                                     Value = valueProperty(item)
                                                 }).ToList();
        }
    }
}