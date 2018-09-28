using MyProject.Core.Entities;
using MyProject.Services.MvcPager;
using MyProject.Services.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Data.Daos
{
    [DbFactory("MyP")]
    public class QRTZ_JOB_DETAILSDao : BaseDao<QRTZ_JOB_DETAILS>
    {
        public PagedList<QRTZ_JOB_DETAILS> GetPagedListJob(string jname, int pageIndex, int pageSize)
        {
            var sql = Sql.Builder;
            if (!string.IsNullOrEmpty(jname))
            {
                sql.Where("JOB_NAME=@0", jname);
            }
            sql.OrderBy("SCHED_NAME"); 
            return PagedList<QRTZ_JOB_DETAILS>(pageIndex, pageSize, sql);
        }

        public List<QRTZ_JOB_DETAILS> GetListJobName()
        {
            var sql = Sql.Builder.Select("JOB_NAME").From("QRTZ_JOB_DETAILS").OrderBy("SCHED_NAME");
            return Query<QRTZ_JOB_DETAILS>(sql).ToList();
        }

        public QRTZ_JOB_DETAILS GetJob( string  jname)
        {
            var sql = Sql.Builder.Where("JOB_NAME=@0",jname);
            return FirstOrDefault<QRTZ_JOB_DETAILS>(sql);
        }

        public void DelJob( string jname)
        {
            var sql = Sql.Builder.Append("delete QRTZ_JOB_DETAILS where JOB_NAME=@0",jname);
            Execute(sql);
        }

        public int AddJob(QRTZ_JOB_DETAILS model)
        {
            var sql = Sql.Builder.Append("insert into QRTZ_JOB_DETAILS(SCHED_NAME,JOB_NAME,JOB_GROUP,DESCRIPTION,JOB_CLASS_NAME,IS_DURABLE,IS_NONCONCURRENT,IS_UPDATE_DATA,REQUESTS_RECOVERY)values(@0,@1,@2,@3,@4,@5,@6,@7,@8)"
                ,model.SCHED_NAME,model.JOB_NAME,model.JOB_GROUP,model.DESCRIPTION,model.JOB_CLASS_NAME,model.IS_DURABLE,model.IS_NONCONCURRENT,model.IS_UPDATE_DATA,model.REQUESTS_RECOVERY);
            return Execute(sql);
        }


        public int UpdateJob(QRTZ_JOB_DETAILS model)
        {
            var sql = Sql.Builder.Append("update QRTZ_JOB_DETAILS set [DESCRIPTION]=@0,JOB_CLASS_NAME=@1 where JOB_NAME=@2"
                , model.DESCRIPTION, model.JOB_CLASS_NAME, model.JOB_NAME);
            return Execute(sql);
        }
    }
}
