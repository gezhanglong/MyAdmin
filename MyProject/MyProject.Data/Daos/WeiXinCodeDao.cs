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
    public class WeiXinCodeDao : BaseDao<WeiXinCode>
    {
        public WeiXinCode GetByUnionid(string unionid)
        {
            var sql = Sql.Builder.Where("UnionId=@0",unionid);
            return FirstOrDefault<WeiXinCode>(sql);
        }
    }
}
