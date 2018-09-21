using MyProject.Core.Entities;
using MyProject.Data.Daos;
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

        #region MyRegion
        private readonly JobControllDao _dao = new JobControllDao();

        public List<JobControll> GetAll()
        {
            return _dao.GetAll();
        }

        public JobControll GetByJobName(string jobName)
        {
            return _dao.GetByJobName(jobName);
        }

        public void UpdateJob(JobControll model)
        {
            _dao.Update(model);
        }

        /// <summary>
        /// 更新所有 正在运行 状态为停止
        /// </summary>
        /// <returns></returns>
        public void UpdateAllIsTimeing()
        {
            _dao.UpdateAllIsTimeing();
        } 
        #endregion
         
    }

    public  class MyJobTask : IJob
    {
        

        public void Execute(IJobExecutionContext context)
        {
            var  log = new LogTask();
            log.AddLog(new Core.Entities.Log() { CreateTime = DateTime.Now, Msg = "job,start:" + DateTime.Now, Ret = 0 });
        }

    }
}
