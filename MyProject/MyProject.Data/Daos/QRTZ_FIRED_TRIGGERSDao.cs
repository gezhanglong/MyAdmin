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
    public class QRTZ_FIRED_TRIGGERSDao : BaseDao<QRTZ_FIRED_TRIGGERS>
    {
        public List<QRTZ_FIRED_TRIGGERS> GetListFiredTriggers()
        {
            var sql = Sql.Builder.Select("*").From("QRTZ_FIRED_TRIGGERS");
            return Query<QRTZ_FIRED_TRIGGERS>(sql).ToList();
        }
    }
}
