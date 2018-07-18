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
    public class WeiXinUserDao : BaseDao<WeiXinUser>
    {
        public PagedList<WeiXinUser> GetPagedList(string fields, string fieldValue, string sdate, string edate, bool IsLike, int pageIndex, int pageSize)
        {
            var sql = Sql.Builder;
            if (!string.IsNullOrEmpty(fields))
            {
                if (IsLike)
                {
                    sql.Where(fields + "='" + fieldValue + "' ");
                }
                else
                {
                    sql.Where(fields + " like'%" + fieldValue + "%' ");
                }
            } 
            if (!string.IsNullOrEmpty(sdate) && string.IsNullOrEmpty(edate))
            {
                sql.Where("createtime>=@0", sdate);
            }
            if (string.IsNullOrEmpty(sdate) && !string.IsNullOrEmpty(edate))
            {
                sql.Where("createtime<@0", edate);
            }
            if (!string.IsNullOrEmpty(sdate) && !string.IsNullOrEmpty(edate))
            {
                sql.Where("createtime>=@0 and createtime<@1", sdate, edate);
            }
            sql.OrderBy("createtime desc");
            return PagedList<WeiXinUser>(pageIndex, pageSize, sql);
        }

        public WeiXinUser GetByOpenId(string openId)
        {
            var sql = Sql.Builder.Where("OpenId=@0", openId);
            return FirstOrDefault<WeiXinUser>(sql); 
        }
    }
}
