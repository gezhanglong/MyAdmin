using System;
using System.Data;
using System.Web;
using System.Web.Mvc;
using MyProject.Data;
using MyProject.Services.Utility;
using MyProject.Controllers.Core;

namespace MyProject.Controllers.SqlSearch
{
    public class SqlSearchController : BaseController
    {
        [SupportFilter]
        public ActionResult Index()
        { 
            ViewData["Sql"] = string.Empty;
            var dataTable = new DataTable();
            return View(dataTable);
        }

        [SupportFilter]
        [HttpPost]
        public ActionResult Index(string sql, string a)
        {
            try
            {
                ViewData["Sql"] = string.Empty;
                if (string.IsNullOrEmpty(sql))
                    return View("Index");

                var dt = QueryHelper.ExecSql("MyP", sql).Tables[0];
                ViewData["Sql"] = Decode(sql);
                return View(dt);
            }
            catch (Exception ex)
            {
                return AlertMsg(
                    ex.Message.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\'", "\\'").Replace("\"", "\\\""), "/SqlSearch/Index?sql=" + Encode(sql));
            }
        }

        private string Encode(string sql)
        {
            return HttpUtility.HtmlEncode(sql.Replace("'", "Comma").Replace("\"", "Comma")).Replace("\r\n", "<br />");
        }

        private string Decode(string sql)
        {
            return HttpUtility.HtmlDecode(sql).Replace("<br />", "\r\n").Replace("Comma", "'");
        }
    }
}