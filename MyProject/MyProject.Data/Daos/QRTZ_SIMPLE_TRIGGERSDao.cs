using MyProject.Core.Dtos;
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
    public class QRTZ_SIMPLE_TRIGGERSDao : BaseDao<QRTZ_SIMPLE_TRIGGERS>
    {
        public void DelSimpleTriggers(string tname)
        {
            var sqls = Sql.Builder.Append("delete QRTZ_SIMPLE_TRIGGERS where TRIGGER_NAME=@0", tname);
            Execute(sqls); 
        }

        public void AddSimpleTriggers(QRTZ_TRIGGERSDto model)
        { 
            var sqls = Sql.Builder.Append("insert into QRTZ_SIMPLE_TRIGGERS(SCHED_NAME,TRIGGER_NAME,TRIGGER_GROUP,REPEAT_COUNT,REPEAT_INTERVAL,TIMES_TRIGGERED) values(@0,@1,@2,@3,@4,@5)"
              , model.SCHED_NAME, model.TRIGGER_NAME, model.TRIGGER_GROUP, model.REPEAT_COUNT, model.REPEAT_INTERVAL, model.TIMES_TRIGGERED);
            Execute(sqls);
        }


        public void UpdateSimpleTriggers(QRTZ_TRIGGERSDto model)
        { 
            var sqls = Sql.Builder.Append("update QRTZ_SIMPLE_TRIGGERS set REPEAT_COUNT=@0,REPEAT_INTERVAL=@1 where TRIGGER_NAME=@2"
              , model.REPEAT_COUNT, model.REPEAT_INTERVAL, model.TRIGGER_NAME);
            Execute(sqls);
        }
    }
}
