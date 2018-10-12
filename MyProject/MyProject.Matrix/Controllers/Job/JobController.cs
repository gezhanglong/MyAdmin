using MyProject.Core.Entities;
using MyProject.Core.Enums;
using MyProject.Matrix.Controllers.Core;
using MyProject.Matrix.Controllers.Job.ViewModels;
using MyProject.Services.Mappers;
using MyProject.Task;
using Quartz;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyProject.Services.Extensions;
using MyProject.Core.Dtos;
using Deepleo.Weixin.SDK;
using Deepleo.Weixin.SDK.JSSDK;

namespace MyProject.Matrix.Controllers.Job
{
    public class JobController : BaseController
    {
        private readonly JobControllTask _job = new JobControllTask();

        #region Job操作
        [SupportFilter]
        public ActionResult JobList(string jname,int pageIndex = 1, int pageSize = 5)
        {
            ViewBag.perm = GetPermission();
            var model = _job.GetPagedListJob(jname,pageIndex, pageSize); 
            return View(model);
        }
         
        
        [SupportFilter]
        public ActionResult JobSave(string jname)
        {
            var jobgroupList =JobGroupEnum.JobGroup1.ToSelectList();
            jobgroupList.Insert(0, new SelectListItem { Text = "请选择", Value = string.Empty });
            ViewData["JobGroupList"] = jobgroupList;
            var schedulerList = _job.GetListScheduler().ToSelectList(c => c.SCHED_NAME, c => c.SCHED_NAME);
            schedulerList.Insert(0, new SelectListItem { Text = "请选择", Value = string.Empty });
            ViewData["schedulerList"] = schedulerList;

            var model = new JobModel(); 
            if (!string.IsNullOrEmpty(jname))
            {
                var job = _job.GetJob(jname);
                if (job == null)
                    return AlertMsg("参数错误", HttpContext.Request.UrlReferrer.PathAndQuery);

                model = EntityMapper.Map<QRTZ_JOB_DETAILS, JobModel>(job); 
            }
            return View(model);
        }
        [SupportFilter]
        [HttpPost]
        public ActionResult JobSave(JobModel savemodel)
        {
            var jobgroupList = JobGroupEnum.JobGroup1.ToSelectList();
            jobgroupList.Insert(0, new SelectListItem { Text = "请选择", Value = string.Empty });
            ViewData["JobGroupList"] = jobgroupList;
            var schedulerList = _job.GetListScheduler().ToSelectList(c => c.SCHED_NAME, c => c.SCHED_NAME);
            schedulerList.Insert(0, new SelectListItem { Text = "请选择", Value = string.Empty });
            ViewData["schedulerList"] = schedulerList;

            var model = _job.GetJob(savemodel.JOB_NAME); 
            if (model==null)
            {  
                if (ModelState.IsValid)
                {
                    model = new QRTZ_JOB_DETAILS
                    {
                        SCHED_NAME = savemodel.SCHED_NAME,
                        JOB_NAME = savemodel.JOB_NAME,
                        JOB_GROUP = savemodel.JOB_GROUP,
                        JOB_CLASS_NAME = savemodel.JOB_CLASS_NAME,
                        DESCRIPTION=savemodel.DESCRIPTION, 
                        JOB_DATA=new byte[0]
                    };
                   var result=  _job.AddJob(model);
                   if (result.Ret == -1)
                    {
                        ModelState.AddModelError("JOB_NAME", result.Msg);
                        return View(savemodel);
                    }
                   return CloseParentBox("保存成功", "/Job/JobList");
                }
            }
            else
            { 
                model.JOB_CLASS_NAME = savemodel.JOB_CLASS_NAME;
                model.DESCRIPTION = savemodel.DESCRIPTION;

                var result = _job.UpdateJob(model);
                if (result.Ret == -1)
                {
                    ModelState.AddModelError("JOB_NAME", result.Msg);
                    return View(savemodel);
                }
                return CloseParentBox("修改成功", "/Job/JobList");
            }
            return View(savemodel);
        }
 
       
        [SupportFilter]
        [HttpPost]
        public ActionResult JobDelete(string jname)
        {
            return Content(_job.DelJob(jname).Msg); 
        } 
        #endregion

        #region Triggers操作
        [SupportFilter]
        public ActionResult TriggerList(string tname, int pageIndex = 1, int pageSize = 5)
        {
            ViewBag.perm = GetPermission();
            var model = _job.GetPagedListTriggers(tname, pageIndex, pageSize);
            return View(model);
        }


        [SupportFilter]
        public ActionResult TriggerSave(string tname)
        {
            #region 下拉初始化
            var triggerStateList = new List<SelectListItem>();
            triggerStateList.Insert(0, new SelectListItem { Text = "等待", Value = "WAITING" });
            triggerStateList.Insert(1, new SelectListItem { Text = "暂停", Value = "PAUSED" });
            ViewData["triggerStateList"] = triggerStateList;

            var triggerTypeList = new List<SelectListItem>();
            triggerTypeList.Insert(0, new SelectListItem { Text = "简单触发器", Value = "SIMPLE" });
            triggerTypeList.Insert(1, new SelectListItem { Text = "CRON触发器", Value = "CRON" });
            ViewData["triggerTypeList"] = triggerTypeList;

            var triggersGroupList = TriggersGroupEnum.TriggersGroup1.ToSelectList();
            triggersGroupList.Insert(0, new SelectListItem { Text = "请选择", Value = string.Empty });
            ViewData["triggersGroupList"] = triggersGroupList;

            var jobList = _job.GetListJobName().ToSelectList(c => c.JOB_NAME, c => c.JOB_NAME);
            jobList.Insert(0, new SelectListItem { Text = "请选择", Value = string.Empty });
            ViewData["jobList"] = jobList; 
            #endregion

            var model = new TriggersModel();
            if (!string.IsNullOrEmpty(tname))
            {
                var trigger = _job.GetTriggers(tname);
                if (trigger == null)
                    return AlertMsg("参数错误", HttpContext.Request.UrlReferrer.PathAndQuery);

                model.TRIGGER_NAME = trigger.TRIGGER_NAME;
                model.TRIGGER_GROUP = trigger.TRIGGER_GROUP;
                model.JOB_NAME = trigger.JOB_NAME;
                model.PRIORITY = trigger.PRIORITY;
                model.DESCRIPTION = trigger.DESCRIPTION;
                model.TRIGGER_STATE = trigger.TRIGGER_STATE;
                model.TRIGGER_TYPE = trigger.TRIGGER_TYPE;
                model.CRON_EXPRESSION = trigger.CRON_EXPRESSION;
                model.REPEAT_COUNT = trigger.REPEAT_COUNT; 
                model.REPEAT_INTERVAL = trigger.REPEAT_INTERVAL / 60/1000.00;
                model.START_TIME= new DateTime(Convert.ToInt64(trigger.START_TIME)).AddHours(8);
                if (trigger.END_TIME != null)
                {
                    model.END_TIME = new DateTime(Convert.ToInt64(trigger.END_TIME)).AddHours(8);
                }  
            }
            else
            {
                model.START_TIME = DateTime.Now;
                model.REPEAT_COUNT = -1;//无限循环
                model.REPEAT_INTERVAL = 5;
            }
            return View(model);
        }
        [SupportFilter]
        [HttpPost]
        public ActionResult TriggerSave(TriggersModel savemodel)
        {
            #region 下拉初始化
            var triggerStateList = new List<SelectListItem>();
            triggerStateList.Insert(0, new SelectListItem { Text = "等待", Value = "WAITING" });
            triggerStateList.Insert(1, new SelectListItem { Text = "暂停", Value = "PAUSED" });
            ViewData["triggerStateList"] = triggerStateList;

            var triggerTypeList = new List<SelectListItem>();
            triggerTypeList.Insert(0, new SelectListItem { Text = "简单触发器", Value = "SIMPLE" });
            triggerTypeList.Insert(1, new SelectListItem { Text = "CRON触发器", Value = "CRON" });
            ViewData["triggerTypeList"] = triggerTypeList;

            var triggersGroupList = TriggersGroupEnum.TriggersGroup1.ToSelectList();
            triggersGroupList.Insert(0, new SelectListItem { Text = "请选择", Value = string.Empty });
            ViewData["triggersGroupList"] = triggersGroupList;

            var jobList = _job.GetListJobName().ToSelectList(c => c.JOB_NAME, c => c.JOB_NAME);
            jobList.Insert(0, new SelectListItem { Text = "请选择", Value = string.Empty });
            ViewData["jobList"] = jobList;
            #endregion

            var model = _job.GetTriggers(savemodel.TRIGGER_NAME);
            var jobModel = _job.GetJob(savemodel.JOB_NAME);
            if (savemodel.TRIGGER_TYPE == "SIMPLE" && savemodel.REPEAT_INTERVAL <= 0)
            {
                ModelState.AddModelError("JOB_NAME", "请填写重复间隔");
                return View(savemodel);
            } 
            if (model == null)
            {
                if (ModelState.IsValid)
                {
                    model = new QRTZ_TRIGGERSDto
                    {
                        SCHED_NAME = jobModel.SCHED_NAME,
                        TRIGGER_NAME = savemodel.TRIGGER_NAME,
                        TRIGGER_GROUP=savemodel.TRIGGER_GROUP,
                        JOB_NAME = jobModel.JOB_NAME,
                        JOB_GROUP = jobModel.JOB_GROUP, 
                        DESCRIPTION = savemodel.DESCRIPTION,
                        NEXT_FIRE_TIME = savemodel.START_TIME.AddHours(-8).AddMilliseconds(savemodel.REPEAT_INTERVAL *60 * 1000).Ticks, 
                        PRIORITY=savemodel.PRIORITY,
                        TRIGGER_STATE=savemodel.TRIGGER_STATE,
                        TRIGGER_TYPE = savemodel.TRIGGER_TYPE,
                        START_TIME = savemodel.START_TIME.AddHours(-8).Ticks, 
                        CRON_EXPRESSION=savemodel.CRON_EXPRESSION,
                        REPEAT_COUNT=savemodel.REPEAT_COUNT,
                        REPEAT_INTERVAL = Convert.ToInt64(savemodel.REPEAT_INTERVAL  * 60 * 1000) 
                    };
                    if (savemodel.END_TIME != null)
                    {
                        model.END_TIME = Convert.ToDateTime(savemodel.END_TIME).AddHours(-8).Ticks;
                    } 
                    var result = _job.AddTriggers(model);
                    if (result.Ret==-1)
                    {
                        ModelState.AddModelError("JOB_NAME", result.Msg);
                        return View(savemodel);
                    }
                    return CloseParentBox(result.Msg, "/Job/TriggerList");
                }
            }
            else
            {
                model.DESCRIPTION = savemodel.DESCRIPTION; 
                model.PRIORITY = savemodel.PRIORITY;
                model.TRIGGER_STATE = savemodel.TRIGGER_STATE;
                model.TRIGGER_TYPE = savemodel.TRIGGER_TYPE;
                model.START_TIME = savemodel.START_TIME.AddHours(-8).Ticks; 
                model.CRON_EXPRESSION = savemodel.CRON_EXPRESSION;
                model.REPEAT_COUNT = savemodel.REPEAT_COUNT;
                model.REPEAT_INTERVAL = Convert.ToInt64(savemodel.REPEAT_INTERVAL *60 * 1000);
                if (savemodel.END_TIME != null)
                {
                    model.END_TIME = Convert.ToDateTime(savemodel.END_TIME).AddHours(-8).Ticks;
                }
                var result = _job.UpdateTriggers(model);
                if (result.Ret == -1)
                {
                    ModelState.AddModelError("JOB_NAME", result.Msg);
                    return View(savemodel);
                }
                return CloseParentBox(result.Msg, "/Job/TriggerList");
            }
            return View(savemodel);
        }


        [SupportFilter]
        [HttpPost]
        public ActionResult TriggerDelete(string tname)
        {
           return  Content(_job.DelTriggers(tname).Msg);
        }
        #endregion

        #region fired_triggers 操作 
        public ActionResult FiredTriggersList()
        { 
            var model = _job.GetListFiredTriggers();
            return View(model);
        } 
        #endregion
         
    }

    /// <summary>
    /// 刷新公众号token
    /// </summary>
    public class WxUpdateToken : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var msg = "";
            var _log = new LogTask(); 
            var _wxconfig = new WeiXinConfigTask();
            var configList = _wxconfig.GetListConfig(); 
            foreach (var item in configList)
            {
                var config = _wxconfig.GetConfig(item.WeiXinId);
                if (config != null)
                {
                    try
                    {
                        item.AccessToken = BasicAPI.GetAccessToken(item.AppId, item.Appsecret).access_token;
                        item.JsApiToken = JSAPI.GetTickect(item.AccessToken).ticket;
                        item.TokenUpdateTime = DateTime.Now;
                        msg= _wxconfig.UpdateToken(item).Msg;
                    }
                    catch (Exception e)
                    {
                        var log = new Log() { CreateTime = DateTime.Now, Msg = "公众号token更新错误：" + e.Message + ";时间：" + DateTime.Now, Ret = 0 };
                        _log.AddLog(log);
                    }
                }

                var log1 = new Log() { CreateTime = DateTime.Now, Msg = "公众号token更新：" + item.WeiXinId + "执行：" + msg + ";时间：" + DateTime.Now, Ret = 0 };
                _log.AddLog(log1);
            }
        }
    }
}
