using MyProject.Core.Dtos;
using MyProject.Core.Entities;
using MyProject.Data.Daos;
using MyProject.Services.MvcPager; 
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
        private readonly QRTZ_TRIGGERSDao _qrtz_triggers = new QRTZ_TRIGGERSDao();
        private readonly QRTZ_SIMPLE_TRIGGERSDao _qrtz_simple_triggers = new QRTZ_SIMPLE_TRIGGERSDao();
        private readonly QRTZ_CRON_TRIGGERSDao _qrtz_cron_triggers = new QRTZ_CRON_TRIGGERSDao();
        private readonly QRTZ_SCHEDULER_STATEDao _qrtz_scheduler_state = new QRTZ_SCHEDULER_STATEDao();
        private readonly QRTZ_FIRED_TRIGGERSDao _qrtz_fired_triggers = new QRTZ_FIRED_TRIGGERSDao();
        private readonly RequestResultDto _result = new RequestResultDto() { Ret=-1,Msg=""};

        #region  Job 操作 
        public PagedList<QRTZ_JOB_DETAILS> GetPagedListJob(string jname, int pageIndex, int pageSize)
        {
            return _qrtz_job_details.GetPagedListJob(jname, pageIndex, pageSize);
        }

        public List<QRTZ_JOB_DETAILS> GetListJobName()
        {
            return _qrtz_job_details.GetListJobName();
        }

        public QRTZ_JOB_DETAILS GetJob(string jname)
        {
            return _qrtz_job_details.GetJob(jname);
        }

        public RequestResultDto DelJob(string jname)
        {
            try
            {
                _qrtz_job_details.DelJob(jname);
                _result.Ret = 0;
                _result.Msg = "删除成功";
            }
            catch (Exception e)
            {
                _result.Msg = e.Message;
            }
            return _result;
           
        }

        public RequestResultDto UpdateJob(QRTZ_JOB_DETAILS model)
        {
            try
            {
                _qrtz_job_details.UpdateJob(model);
                _result.Ret = 0;
                _result.Msg = "修改成功";
            }
            catch (Exception e)
            {
                _result.Msg = e.Message;
            }
            return _result;
           
        }
        public RequestResultDto AddJob(QRTZ_JOB_DETAILS model)
        {
            try
            {
                _qrtz_job_details.AddJob(model);
                _result.Ret = 0;
                _result.Msg = "添加成功";
            }
            catch (Exception e)
            {
                _result.Msg = e.Message;
            }
            return _result; 
        } 
        #endregion

        #region  triggers 操作
        public PagedList<QRTZ_TRIGGERSDto> GetPagedListTriggers(string tname, int pageIndex, int pageSize)
        {
            return _qrtz_triggers.GetPagedListTriggers(tname, pageIndex, pageSize);
        }

        public QRTZ_TRIGGERSDto GetTriggers(string tname)
        {
            return _qrtz_triggers.GetTriggers(tname);
        }

        public RequestResultDto DelTriggers(string tname)
        {
            try
            {
                _qrtz_simple_triggers.DelSimpleTriggers(tname);
                _qrtz_cron_triggers.DelCronTriggers(tname);
                _qrtz_triggers.DelTriggers(tname);
                _result.Ret = 0;
                _result.Msg = "删除成功";
            }
            catch (Exception e)
            {
                _result.Msg = e.Message;
            }
            return _result;
        }

        public RequestResultDto UpdateTriggers(QRTZ_TRIGGERSDto model)
        {
             try
            {
                _qrtz_triggers.UpdateTriggers(model);
                if (model.TRIGGER_TYPE == "SIMPLE")
                {
                    _qrtz_simple_triggers.UpdateSimpleTriggers(model);
                }
                else
                {
                    _qrtz_cron_triggers.UpdateCronTriggers(model);
                }
                _result.Ret = 0;
                _result.Msg = "修改成功";
            }
             catch (Exception e)
             {
                 _result.Msg = e.Message;
             }
             return _result;
        }

        public RequestResultDto AddTriggers(QRTZ_TRIGGERSDto model)
        {
            try
            {
                _qrtz_triggers.AddTriggers(model);
                if (model.TRIGGER_TYPE == "SIMPLE")
                {
                    _qrtz_simple_triggers.AddSimpleTriggers(model);
                }
                else
                {
                    _qrtz_cron_triggers.AddCronTriggers(model);
                } 
                _result.Ret = 0;
                _result.Msg = "添加成功";
            }
            catch (Exception e)
            {
                _result.Msg = e.Message;
            }
            return _result;
        }
        #endregion

        #region  scheduler 操作
        public List<QRTZ_SCHEDULER_STATE> GetListScheduler()
        {
            return _qrtz_scheduler_state.GetListScheduler();
        }
        #endregion

        #region  fired_triggers 操作
        public List<QRTZ_FIRED_TRIGGERS> GetListFiredTriggers()
        {
            return _qrtz_fired_triggers.GetListFiredTriggers();
        }
        #endregion
    }
     
}
