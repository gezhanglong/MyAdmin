using MyProject.Core.Dtos;
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
    public class QRTZ_TRIGGERSDao : BaseDao<QRTZ_TRIGGERS>
    {
        public PagedList<QRTZ_TRIGGERSDto> GetPagedListTriggers(string tname, int pageIndex, int pageSize)
        {
            var sql = Sql.Builder.Select("a.*,b.REPEAT_COUNT,b.REPEAT_INTERVAL,b.TIMES_TRIGGERED,c.CRON_EXPRESSION,c.TIME_ZONE_ID")
                .From("dbo.QRTZ_TRIGGERS  as a")
                .LeftJoin("dbo.QRTZ_SIMPLE_TRIGGERS  as b")
                .On("a.TRIGGER_NAME=b.TRIGGER_NAME and a.TRIGGER_GROUP=b.TRIGGER_GROUP")
                .LeftJoin("dbo.QRTZ_CRON_TRIGGERS as c")
                .On("a.TRIGGER_NAME=c.TRIGGER_NAME and a.TRIGGER_GROUP=c.TRIGGER_GROUP");
            if (!string.IsNullOrEmpty(tname))
            {
                sql.Where("a.TRIGGER_NAME=@0", tname);
            }
            sql.OrderBy("a.SCHED_NAME");
            return PagedList<QRTZ_TRIGGERSDto>(pageIndex, pageSize, sql);
        }

        public QRTZ_TRIGGERSDto GetTriggers(string tname)
        {
            var sql = Sql.Builder.Select("a.*,b.REPEAT_COUNT,b.REPEAT_INTERVAL,b.TIMES_TRIGGERED,c.CRON_EXPRESSION,c.TIME_ZONE_ID")
                .From("dbo.QRTZ_TRIGGERS  as a")
                .LeftJoin("dbo.QRTZ_SIMPLE_TRIGGERS  as b")
                .On("a.TRIGGER_NAME=b.TRIGGER_NAME and a.TRIGGER_GROUP=b.TRIGGER_GROUP")
                .LeftJoin("dbo.QRTZ_CRON_TRIGGERS as c")
                .On("a.TRIGGER_NAME=c.TRIGGER_NAME and a.TRIGGER_GROUP=c.TRIGGER_GROUP")
                .Where("a.TRIGGER_NAME=@0", tname);
            return FirstOrDefault<QRTZ_TRIGGERSDto>(sql);
        }

        public void DelTriggers(string tname)
        { 
            var sqlt = Sql.Builder.Append("delete QRTZ_TRIGGERS where TRIGGER_NAME=@0", tname);
            Execute(sqlt); 
        }

        public void AddTriggers(QRTZ_TRIGGERSDto model)
        {
            var sql = Sql.Builder.Append("insert into QRTZ_TRIGGERS(SCHED_NAME,TRIGGER_NAME,TRIGGER_GROUP,JOB_NAME,JOB_GROUP,DESCRIPTION,NEXT_FIRE_TIME,PREV_FIRE_TIME,PRIORITY,TRIGGER_STATE,TRIGGER_TYPE,START_TIME,END_TIME,MISFIRE_INSTR) values(@0,@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11,@12,@13)"
                , model.SCHED_NAME, model.TRIGGER_NAME, model.TRIGGER_GROUP, model.JOB_NAME, model.JOB_GROUP, model.DESCRIPTION, model.NEXT_FIRE_TIME, model.PREV_FIRE_TIME, model.PRIORITY, model.TRIGGER_STATE, model.TRIGGER_TYPE, model.START_TIME, model.END_TIME, model.MISFIRE_INSTR);
            Execute(sql); 
        }


        public void UpdateTriggers(QRTZ_TRIGGERSDto model)
        {
            var sqlt = Sql.Builder.Append("update QRTZ_TRIGGERS set DESCRIPTION=@0,NEXT_FIRE_TIME=@1,PREV_FIRE_TIME=@2,PRIORITY=@3,TRIGGER_STATE=@4,TRIGGER_TYPE=@5,START_TIME=@6,END_TIME=@7 where TRIGGER_NAME=@8"
                , model.DESCRIPTION,model.NEXT_FIRE_TIME,model.PREV_FIRE_TIME,model.PRIORITY,model.TRIGGER_STATE,model.TRIGGER_TYPE,model.START_TIME,model.END_TIME,model.TRIGGER_NAME);
            Execute(sqlt); 
        }
    }
}
