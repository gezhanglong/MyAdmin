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
    public class QRTZ_SCHEDULER_STATEDao : BaseDao<QRTZ_SCHEDULER_STATE>
    {
         public List<QRTZ_SCHEDULER_STATE> GetListScheduler()
         {
             var sql = Sql.Builder.Select("*").From("QRTZ_SCHEDULER_STATE");
             return Query<QRTZ_SCHEDULER_STATE>(sql).ToList();
         }
    }
}
