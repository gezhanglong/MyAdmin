using MyProject.Matrix.Controllers.Core;
using MyProject.Matrix.Controllers.WeiXinConfig.ViewModels;
using MyProject.Services.Mappers;
using MyProject.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Matrix.Controllers.WeiXinConfig
{
    public class WeiXinConfigController : BaseController
    {
        private readonly WeiXinConfigTask _task = new WeiXinConfigTask();

        #region 操作
        [SupportFilter]
        public ActionResult Index(string weixinId, int pageIndex = 1, int pageSize = 5)
        {
            ViewBag.perm = GetPermission();
            var model = _task.GetPagedListConfig(weixinId, pageIndex, pageSize);
            return View(model);
        }


        [SupportFilter]
        public ActionResult Save(string weixinId)
        {
            var categoryList = new List<SelectListItem>();
            categoryList.Insert(0, new SelectListItem { Text = "公众号", Value = "1" });
            categoryList.Insert(1, new SelectListItem { Text = "小程序", Value = "2" });
            ViewData["categoryList"] = categoryList;

            var model = new ConfigModel();
            if (!string.IsNullOrEmpty(weixinId))
            {
                var job = _task.GetConfig(weixinId);
                if (job == null)
                    return AlertMsg("参数错误", HttpContext.Request.UrlReferrer.PathAndQuery);

                model = EntityMapper.Map<MyProject.Core.Entities.WeiXinConfig, ConfigModel>(job);
            }
            return View(model);
        }
        [SupportFilter]
        [HttpPost]
        public ActionResult Save(ConfigModel savemodel)
        {
            var categoryList = new List<SelectListItem>();
            categoryList.Insert(0, new SelectListItem { Text = "公众号", Value = "1" });
            categoryList.Insert(1, new SelectListItem { Text = "小程序", Value = "2" });
            ViewData["categoryList"] = categoryList;

            var model = _task.GetConfig(savemodel.WeiXinId);
            if (model == null)
            {
                if (ModelState.IsValid)
                {
                    model = new MyProject.Core.Entities.WeiXinConfig
                    {
                        WeiXinName = savemodel.SCHED_NAME,
                        JOB_NAME = savemodel.JOB_NAME,
                        JOB_GROUP = savemodel.JOB_GROUP,
                        JOB_CLASS_NAME = savemodel.JOB_CLASS_NAME,
                        DESCRIPTION = savemodel.DESCRIPTION,
                        JOB_DATA = new byte[0]
                    };
                    var result = _task.AddConfig(model);
                    if (result.Ret == -1)
                    {
                        ModelState.AddModelError("WeiXinName", result.Msg);
                        return View(savemodel);
                    }
                    return CloseParentBox("保存成功", "/WeiXinConfig/Index");
                }
            }
            else
            {
                model.JOB_CLASS_NAME = savemodel.JOB_CLASS_NAME;
                model.DESCRIPTION = savemodel.DESCRIPTION;

                var result = _task.UpdateConfig(model);
                if (result.Ret == -1)
                {
                    ModelState.AddModelError("WeiXinName", result.Msg);
                    return View(savemodel);
                }
                return CloseParentBox("修改成功", "/WeiXinConfig/Index");
            }
            return View(savemodel);
        }


        [SupportFilter]
        [HttpPost]
        public ActionResult Delete(string jname)
        {
            return Content(_task.DelConfig(jname).Msg);
        }
        #endregion 

    }
}
