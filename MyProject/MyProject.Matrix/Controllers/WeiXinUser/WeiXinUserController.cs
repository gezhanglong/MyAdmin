using MyProject.Matrix.Controllers.Core;
using MyProject.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Matrix.Controllers.WeiXinUser
{
    public class WeiXinUserController : BaseController
    {
        private readonly WeiXinUserTask _weiXinUserTask = new WeiXinUserTask();

        [SupportFilter]
        public ActionResult Index(string fields, string sdate, string edate, bool IsLike = true, string fieldValue = "", int pageIndex = 1, int pageSize = 10)
        {
            var fieldList = new List<SelectListItem>();
            fieldList.Insert(0, new SelectListItem { Text = "全部", Value = "" });
            fieldList.Insert(1, new SelectListItem { Text = "openid", Value = "openid" });
            fieldList.Insert(2, new SelectListItem { Text = "昵称", Value = "nickname" });
            fieldList.Insert(3, new SelectListItem { Text = "unionid", Value = "unionid" });
            ViewData["fieldsList"] = fieldList;
            ViewData["fieldValue"] = fieldValue; 
            ViewData["sdate"] = sdate;
            ViewData["edate"] = edate;
            var list = _weiXinUserTask.GetPagedList(fields, fieldValue, sdate, edate, IsLike, pageIndex, pageSize);
            return View(list);
        }
    }
}
