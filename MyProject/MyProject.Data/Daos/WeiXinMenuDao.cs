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
    public class WeiXinMenuDao : BaseDao<WeiXinMenu>
    {
        public List<WeiXinMenu> GetList()
        {
            var sql = Sql.Builder.Where("1=1").OrderBy("menuid");
            return Query<WeiXinMenu>(sql).ToList();
        }

        public WeiXinMenu GetById(int menuId)
        {
            var sql = Sql.Builder.Where("menuid=@0",menuId);
            return FirstOrDefault<WeiXinMenu>(sql);
        }

        public void Delete(int menuId)
        {
            var sql = Sql.Builder.Append("delete WeiXinMenu where menuid=@0",menuId);
            Execute(sql);
        }
    }
}
