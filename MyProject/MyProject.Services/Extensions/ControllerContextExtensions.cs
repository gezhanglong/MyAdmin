using System.Web.Mvc;

namespace MyProject.Services.Extensions
{
    public static class ControllerContextExtensions
    {
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetPara(this ControllerContext controllerContext, string key)
        {
            if (controllerContext.RouteData.Values.ContainsKey(key))
            {
                return controllerContext.RouteData.Values[key].ToString();
            }
            if (!string.IsNullOrEmpty(controllerContext.HttpContext.Request.QueryString[key]))
            {
                return controllerContext.HttpContext.Request.QueryString[key];
            }
            if (!string.IsNullOrEmpty(controllerContext.HttpContext.Request.Form[key]))
            {
                return controllerContext.HttpContext.Request.Form[key];
            }
            return string.Empty;
        }
    }
}