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
    public class LogDao : BaseDao<Log>
    {
        public List<Log> GetList()
        {
            var sql = Sql.Builder.Where("1=1");
            return Query<Log>(sql).ToList();
        }
    }
}
