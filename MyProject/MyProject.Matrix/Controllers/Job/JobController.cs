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

namespace MyProject.Matrix.Controllers.Job
{
    public class JobController : BaseController
    {
        private readonly JobControllTask _job = new JobControllTask();

        #region Job列表
        [SupportFilter]
        public ActionResult JobList(string jname,int pageIndex = 1, int pageSize = 5)
        {
            ViewBag.perm = GetPermission();
            var model = _job.GetPagedList(jname,pageIndex, pageSize); 
            return View(model);
        }

        #endregion

        #region 保存/修改Job信息 Save
        [SupportFilter]
        public ActionResult JobSave(string jname)
        {
            ViewData["JobGroupList"] = JobGroupEnum.JobGroup1.ToSelectList();

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
                   if (result <=0)
                    {
                        ModelState.AddModelError("JOB_NAME", "保存失败");
                        return View(savemodel);
                    }
                   return CloseParentBox("保存成功", "/Job/JobList");
                }
            }
            else
            {
                model.SCHED_NAME = savemodel.SCHED_NAME;
                model.JOB_GROUP = savemodel.JOB_GROUP;
                model.JOB_CLASS_NAME = savemodel.JOB_CLASS_NAME;
                model.DESCRIPTION = savemodel.DESCRIPTION;

                _job.UpdateJob(model);
                return CloseParentBox("修改成功", "/Job/JobList");
            }
            return View(savemodel);
        }

        #endregion

        #region 删除
        [SupportFilter]
        [HttpPost]
        public void JobDelete(string jname)
        {
            _job.DelJob(jname);
        }

        #endregion

    }
   
}
