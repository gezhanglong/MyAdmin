using MyProject.Core.Entities;
using MyProject.Data.Daos;
using MyProject.Services.MvcPager;
using Quartz;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Task
{
    public   class JobControllTask 
    { 
        //#region MyRegion
        //private readonly JobControllDao _dao = new JobControllDao();

        //public List<JobControll> GetAll()
        //{
        //    return _dao.GetAll();
        //}

        //public JobControll GetByJobName(string jobName)
        //{
        //    return _dao.GetByJobName(jobName);
        //}

        //public void UpdateJob(JobControll model)
        //{
        //    _dao.Update(model);
        //}

        ///// <summary>
        ///// 更新所有 正在运行 状态为停止
        ///// </summary>
        ///// <returns></returns>
        //public void UpdateAllIsTimeing()
        //{
        //    _dao.UpdateAllIsTimeing();
        //} 
        //#endregion

        private readonly QRTZ_JOB_DETAILSDao _qrtz_job_details = new QRTZ_JOB_DETAILSDao();

        public PagedList<QRTZ_JOB_DETAILS> GetPagedList(string jname, int pageIndex, int pageSize)
        {
            return _qrtz_job_details.GetPagedList(jname, pageIndex, pageSize);
        }

         public QRTZ_JOB_DETAILS GetJob(string  jname)
        {
           return _qrtz_job_details.GetJob(jname);
        }

         public void DelJob(string jname)
         {
             _qrtz_job_details.DelJob(jname);
         }

         public void UpdateJob(QRTZ_JOB_DETAILS model)
         {
             _qrtz_job_details.UpdateJob(model);
         }
         public int AddJob(QRTZ_JOB_DETAILS model)
         {
             try {
                 return Convert.ToInt32(_qrtz_job_details.AddJob(model));
             }
             catch (Exception e)
             {
                 return 0;
             }
             
         }
    }
     
}
