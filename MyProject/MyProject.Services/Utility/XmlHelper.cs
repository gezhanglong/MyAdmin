using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyProject.Services.Utility
{
    public class XmlHelper
    {
        #region 反序列化
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="xml">XML字符串</param>
        /// <returns></returns>
        public static object Deserialize(Type type, string xml)
        {
            try
            {
                using (StringReader sr = new StringReader(xml))
                {
                    XmlSerializer xmldes = new XmlSerializer(type);
                    return xmldes.Deserialize(sr);
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static object Deserialize(Type type, Stream stream)
        {
            XmlSerializer xmldes = new XmlSerializer(type);
            return xmldes.Deserialize(stream);
        }
        #endregion

        #region 序列化
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string Serializer(Type type, object obj)
        {
            MemoryStream Stream = new MemoryStream();
            XmlSerializer xml = new XmlSerializer(type);
            try
            {
                //序列化对象
                xml.Serialize(Stream, obj);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            Stream.Position = 0;
            StreamReader sr = new StreamReader(Stream);
            string str = sr.ReadToEnd();

            sr.Dispose();
            Stream.Dispose();

            return str;
        }

        #endregion

        /// <summary>
        /// 实体对象转换到Xml
        /// </summary>
        /// <typeparam name="T">实体对象类型</typeparam>
        /// <param name="entity">实体对象实例</param>
        /// <returns>xml</returns>
        public static string EntityToXml<T>(T entity)
        {
            return Serializer(typeof(T), entity);
        }

        /// <summary>
        /// Xml转换到实体对象
        /// </summary>
        /// <typeparam name="T">实体对象类型</typeparam>
        /// <param name="xml">xml</param>
        /// <returns>实体对象类型</returns>
        public static T XmlToEntity<T>(string xml)
        {
            return (T)Deserialize(typeof(T), xml);
        }

        /// <summary>
        /// DataTable转换到Xml
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTableToXml(DataTable dt)
        {
            return Serializer(typeof(DataTable), dt);
        }

        /// <summary>
        /// Xml转换到DataTable
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static DataTable XmltoDataTable(string xml)
        {
            return (DataTable)Deserialize(typeof(DataTable), xml);
        }

        /// <summary>
        /// List转换到Xml
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ListToXml<T>(T list)
        {
            return Serializer(typeof(List<T>), list);
        }

        /// <summary>
        /// Xml转换到List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static List<T> XmlToList<T>(string xml)
        {
            return (List<T>)Deserialize(typeof(T), xml);
        }
    }
}
