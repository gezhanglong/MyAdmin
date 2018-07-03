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
    public class WeiXinReceiveMessageDao : BaseDao<WeiXinReceiveMessage>
    {
        public PagedList<WeiXinReceiveMessage> GetPagedList(string fields, string fieldValue, string sdate, string edate, string ContentValue, bool IsLike, int pageIndex, int pageSize)
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
            if (!string.IsNullOrEmpty(ContentValue))
            {
                sql.Where("Content like'%"+ContentValue+"%'");
            }
            if (!string.IsNullOrEmpty(sdate) && string.IsNullOrEmpty(edate))
            {
                sql.Where("CreateDate>=@0", sdate);
            }
            if (string.IsNullOrEmpty(sdate) && !string.IsNullOrEmpty(edate))
            {
                sql.Where("CreateDate<@0", edate);
            }
            if (!string.IsNullOrEmpty(sdate) && !string.IsNullOrEmpty(edate))
            {
                sql.Where("CreateDate>=@0 and CreateDate<@1", sdate, edate);
            }
            sql.OrderBy("CreateDate desc");
            return PagedList<WeiXinReceiveMessage>(pageIndex, pageSize, sql);
        }
    }
}
