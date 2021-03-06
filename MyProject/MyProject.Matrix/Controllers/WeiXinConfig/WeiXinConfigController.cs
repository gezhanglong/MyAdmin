﻿using MyProject.Matrix.Controllers.Core;
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
        public ActionResult Index(string wxId, int pageIndex = 1, int pageSize = 5)
        {
            ViewBag.perm = GetPermission();
            var model = _task.GetPagedListConfig(wxId, pageIndex, pageSize);
            return View(model);
        }


        [SupportFilter]
        public ActionResult Save(string wxId)
        {
            var categoryList = new List<SelectListItem>();
            categoryList.Insert(0, new SelectListItem { Text = "请选择", Value = string.Empty });
            categoryList.Insert(1, new SelectListItem { Text = "公众号", Value = "1" });
            categoryList.Insert(2, new SelectListItem { Text = "小程序", Value = "2" });
            ViewData["categoryList"] = categoryList;

            var model = new ConfigModel();
            if (!string.IsNullOrEmpty(wxId))
            {
                var job = _task.GetConfig(wxId);
                if (job == null)
                    return AlertMsg("参数错误", HttpContext.Request.UrlReferrer.PathAndQuery);

                model = EntityMapper.Map<MyProject.Core.Entities.WeiXinConfig, ConfigModel>(job);
                model.Id = job.Category;
            }
            return View(model);
        }
        [SupportFilter]
        [HttpPost]
        public ActionResult Save(ConfigModel savemodel)
        {
            var categoryList = new List<SelectListItem>();
            categoryList.Insert(0, new SelectListItem { Text = "请选择", Value = string.Empty });
            categoryList.Insert(1, new SelectListItem { Text = "公众号", Value = "1" });
            categoryList.Insert(2, new SelectListItem { Text = "小程序", Value = "2" });
            ViewData["categoryList"] = categoryList;

            var model = _task.GetConfig(savemodel.WeiXinId);
           
            if (savemodel.Id <=0)
            {
                if (model !=null && model.WeiXinId == savemodel.WeiXinId)
                {
                    ModelState.AddModelError("WeiXinName", "微信号已存在");
                    return View(savemodel);
                }
                var check = _task.Check(savemodel.WeiXinName, savemodel.AppId, savemodel.ApiToken);
                if (check !=null)
                {
                    ModelState.AddModelError("WeiXinName", "appid或微信名称或接口token已存在");
                    return View(savemodel);
                } 
                if (ModelState.IsValid)
                {
                    model = new MyProject.Core.Entities.WeiXinConfig
                    {
                        WeiXinName = savemodel.WeiXinName,
                        WeiXinId = savemodel.WeiXinId,
                        Category = savemodel.Category,
                        ApiToken = savemodel.ApiToken,
                        ApiUrl = savemodel.ApiUrl,
                        AppId = savemodel.AppId,
                        Appsecret = savemodel.Appsecret,
                        MchId = savemodel.MchId,
                        PartnerKey = savemodel.PartnerKey,
                        CertUrl = savemodel.CertUrl,
                        Remark = savemodel.Remark,
                        Creater = GetCurrentAdmin(),
                        CreateTime = DateTime.Now
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
                model.ApiToken = savemodel.ApiToken;
                model.ApiUrl = savemodel.ApiUrl; 
                model.MchId = savemodel.MchId;
                model.PartnerKey = savemodel.PartnerKey;
                model.CertUrl = savemodel.CertUrl;
                model.Remark = savemodel.Remark;
                model.Creater = GetCurrentAdmin();
                model.CreateTime = DateTime.Now;

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
        public ActionResult Delete(string wxId)
        {
            return Content(_task.DelConfig(wxId).Msg);
        }
        #endregion 

    }
}
