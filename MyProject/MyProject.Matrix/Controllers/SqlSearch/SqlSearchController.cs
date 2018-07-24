using System;
using System.Data;
using System.Web;
using System.Web.Mvc; 
using MyProject.Services.Utility;
using MyProject.Matrix.Controllers.Core;
using MyProject.Matrix.Controllers;
using MyProject.Core.Dtos;
using MyProject.Services.Utility;

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

        //导出
        [HttpPost]
        public ActionResult OutFile(string sql)
        {
            try
            { 
                if (string.IsNullOrEmpty(sql))
                    return Json(new RequestResultDto() { Msg = "sql语句不能为空", Ret = -1 });
                
                var dt = QueryHelper.ExecSql("MyP", sql).Tables[0];
                string path = @"E:\"; 
                var msg = "导出成功，导出路径：" + path;
                NpoiSdk.OutFile(dt, path,ref msg);
                return Json(new RequestResultDto() { Msg = msg, Ret = 0 });
            }
            catch (Exception ex)
            {
                return Json(new RequestResultDto() { 
                    Msg=ex.Message.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\'", "\\'").Replace("\"", "\\\""),
                    Ret=-1
                    });
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