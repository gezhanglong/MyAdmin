using MyProject.Controllers.Core;
using MyProject.Task;
using System.Web;
using System.Web.Mvc;

namespace MyProject
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ExceptionLogAttrbute()); //filters.Add(new HandleErrorAttribute());
        }
    }
}