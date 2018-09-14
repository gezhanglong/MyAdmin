using MyProject.Core.Entities;
using MyProject.Services.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Data.Daos
{
    [DbFactory("MyP")]
    public class JobControllDao : BaseDao<JobControll>
    {
        public List<JobControll> GetAll()
        {
            var sql = Sql.Builder.Where("1=1");
            return Query(sql).ToList();
        }

        public JobControll GetByJobName(string jobName)
        {
            var sql = Sql.Builder.Where("JobName=@0",jobName);
            return FirstOrDefault(sql);
        }

        /// <summary>
        /// 更新所有 正在运行 状态为停止
        /// </summary>
        /// <returns></returns>
        public void UpdateAllIsTimeing()
        {
            var sql = Sql.Builder.Append("update JobControll set IsTimeing=0");
            Execute(sql);
        }
    }
}
