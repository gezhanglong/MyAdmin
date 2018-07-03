using System.Web;

namespace MyProject.Services.Extensions
{
   public static class HttpPostedFileBaseExtensions
    {
       /// <summary>
       /// 是否存在文件
       /// </summary>
       /// <param name="file"></param>
       /// <returns></returns>
       public static bool HasFile(this HttpPostedFileBase file)
       {
           return (file != null && file.ContentLength > 0) ? true : false;
       } 
       /// <summary>
       /// 是否存在文件
       /// </summary>
       /// <param name="file"></param>
       /// <returns></returns>
       public static bool HasFile(this HttpPostedFile file)
       {
           return (file != null && file.ContentLength > 0) ? true : false;
       }
    }
}
