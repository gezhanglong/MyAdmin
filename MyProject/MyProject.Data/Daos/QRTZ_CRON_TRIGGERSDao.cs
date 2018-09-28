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
    public class QRTZ_CRON_TRIGGERSDao : BaseDao<QRTZ_CRON_TRIGGERS>
    {
        public void DelCronTriggers(string tname)
        { 
            var sqlc = Sql.Builder.Append("delete QRTZ_CRON_TRIGGERS where TRIGGER_NAME=@0", tname);
            Execute(sqlc); 
        }

        public void AddCronTriggers(QRTZ_TRIGGERSDto model)
        { 
            var sqlc = Sql.Builder.Append("insert into QRTZ_CRON_TRIGGERS(SCHED_NAME,TRIGGER_NAME,TRIGGER_GROUP,CRON_EXPRESSION,TIME_ZONE_ID) values(@0,@1,@2,@3,@4)"
               , model.SCHED_NAME, model.TRIGGER_NAME, model.TRIGGER_GROUP, model.CRON_EXPRESSION, model.TIME_ZONE_ID);
            Execute(sqlc); 
        }


        public void UpdateCronTriggers(QRTZ_TRIGGERSDto model)
        { 
            var sqlc = Sql.Builder.Append("update QRTZ_CRON_TRIGGERS setCRON_EXPRESSION=@0,TIME_ZONE_ID=@1 where TRIGGER_NAME=@2"
              , model.CRON_EXPRESSION, model.TIME_ZONE_ID, model.TRIGGER_NAME);
            Execute(sqlc); 
        }
    }
}
