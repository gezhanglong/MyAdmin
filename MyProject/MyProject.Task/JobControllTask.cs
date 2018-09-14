using MyProject.Core.Entities;
using MyProject.Data.Daos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Task
{
    public class JobControllTask
    {
        private readonly JobControllDao _dao = new JobControllDao();

        public List<JobControll> GetAll()
        {
           return  _dao.GetAll();
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
    }
}
