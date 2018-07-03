#region using

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

#endregion

namespace MyProject.Services.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// 获得某个Enum类型的绑定列表
        /// </summary>
        /// <returns>
        /// 返回一个DataTable
        /// DataTable有两列:"Text": System.String;"Value": System.Int32       
        /// </returns>
        public static Dictionary<string, int> ToDictionary(this Enum en)
        {
            var enumType = en.GetType();
            //建立DataTable的列信息
            var dic = new Dictionary<string, int>();

            //获得特性Description的类型信息
            Type typeDescription = typeof(DescriptionAttribute);

            //获得枚举的字段信息（因为枚举的值实际上是一个static的字段的值）
            System.Reflection.FieldInfo[] fields = enumType.GetFields();

            //检索所有字段
            foreach (FieldInfo field in fields)
            {
                //过滤掉一个不是枚举值的，记录的是枚举的源类型
                if (field.FieldType.IsEnum == true)
                {
                    int value=-1;
                    string text = string.Empty;
                    // 通过字段的名字得到枚举的值
                    // 枚举的值如果是long的话，ToChar会有问题，但这个不在本文的讨论范围之内
                    value = (int)enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null);

                    //获得这个字段的所有自定义特性，这里只查找Description特性
                    object[] arr = field.GetCustomAttributes(typeDescription, true);
                    if (arr.Length > 0)
                    {
                        //因为Description这个自定义特性是不允许重复的，所以我们只取第一个就可以了！
                        DescriptionAttribute aa = (DescriptionAttribute)arr[0];
                        //获得特性的描述值，也就是‘男’‘女’等中文描述
                        text = aa.Description;
                    }
                    else
                    {
                        //如果没有特性描述（-_-!忘记写了吧~）那么就显示英文的字段名
                        text = field.Name;
                    }
                    dic.Add(text, value);
                }
            }

            return dic;
        }

        ///<summary>
        ///  Returns the value of the description attribute attached to an enum value.
        ///</summary>
        ///<param name = "en"></param>
        ///<returns>The text from the System.ComponentModel.DescriptionAttribute associated with the enumeration value.</returns>
        ///<remarks>
        ///  To use this, create an enum and mark its members with a [Description("My Descr")] attribute.
        ///  Then when you call this extension method, you will receive "My Descr".
        ///</remarks>
        ///<example>
        ///  <code>
        ///    enum MyEnum {
        ///    [Description("Some Descriptive Text")]
        ///    EnumVal1,
        ///
        ///    [Description("Some More Descriptive Text")]
        ///    EnumVal2
        ///    }
        /// 
        ///    static void Main(string[] args) {
        ///    Console.PrintLine( MyEnum.EnumVal1.GetDescription() );
        ///    }
        ///  </code>
        /// 
        ///  This will result in the output "Some Descriptive Text".
        ///</example>
        public static string GetDescription(this Enum en)
        {
            var type = en.GetType();
            var memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                var attrs = memInfo[0].GetCustomAttributes(typeof (DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute) attrs[0]).Description;
            }
            return en.ToString();
        }

        /// <summary>
        ///   Converts the <see cref = "Enum" /> type to an <see cref = "IList" /> 
        ///   compatible object.
        /// </summary>
        /// <param name = "type">The <see cref = "Enum" /> type.</param>
        /// <returns>An <see cref = "IList" /> containing the enumerated
        ///   type value and description.</returns>
        public static IList ToList(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (!type.IsEnum)
            {
                throw new ArgumentException("必须为枚举类型", "type");
            }

            var list = new ArrayList();
            var enumValues = Enum.GetValues(type);

            foreach (Enum value in enumValues)
            {
                list.Add(new KeyValuePair<Enum, string>(value, GetDescription(value)));
            }

            return list;
        }

        /// <summary>
        ///   Converts the <see cref = "Enum" /> type to an <see cref = "IList" /> compatible object.
        /// </summary>
        /// <param name = "type">The <see cref = "Enum" /> type.</param>
        /// <returns>An <see cref = "IList" /> containing the enumerated type value and description.</returns>
        public static IList ToList<T>(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (!type.IsEnum)
            {
                throw new ArgumentException("必须为枚举类型", "type");
            }

            var list = new ArrayList();
            var enumValues = Enum.GetValues(type);

            foreach (Enum value in enumValues)
            {
                list.Add(new KeyValuePair<T, string>((T) Convert.ChangeType(value, typeof (T), CultureInfo.InvariantCulture), GetDescription(value)));
            }

            return list;
        }

        /// <summary>
        ///   Converts the <see cref = "Enum" /> type to an <see cref = "IList" /> compatible object.
        /// </summary>
        /// <param name = "type">The <see cref = "Enum" /> type.</param>
        /// <returns>An <see cref = "IList" /> containing the enumerated type value and description.</returns>
        public static IList ToExtendedList<T>(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (!type.IsEnum)
            {
                throw new ArgumentException("必须为枚举类型", "type");
            }

            ArrayList list = new ArrayList();
            Array enumValues = Enum.GetValues(type);

            foreach (Enum value in enumValues)
            {
                list.Add(new KeyValueTriplet<Enum, T, string>(value, (T) Convert.ChangeType(value, typeof (T), CultureInfo.InvariantCulture), GetDescription(value)));
            }

            return list;
        }
    }
}