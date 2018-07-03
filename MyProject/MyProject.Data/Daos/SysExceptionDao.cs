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
    public class SysExceptionDao : BaseDao<SysException>
    {
        public PagedList<SysException> GetPagedList(string edate, string sdate, string helpLink, int pageIndex, int pageSize)
        {
            var sql = Sql.Builder;
            if (!string.IsNullOrEmpty(helpLink))
            {
                sql.Where("HelpLink like'%"+helpLink+"%'");
            }
            if (!string.IsNullOrEmpty(sdate) && string.IsNullOrEmpty(edate))
            {
                sql.Where("CreateTime>=@0", sdate);
            }
            if (string.IsNullOrEmpty(sdate) && !string.IsNullOrEmpty(edate))
            {
                sql.Where("CreateTime<@0", edate);
            }
            if (!string.IsNullOrEmpty(sdate) && !string.IsNullOrEmpty(edate))
            {
                sql.Where("CreateTime>=@0 and CreateTime<@1", sdate, edate);
            }
            sql.OrderBy("Createtime desc");
            return PagedList<SysException>(pageIndex,pageSize,sql);
        }

        public SysException GetExt(int id)
        {
            var sql = Sql.Builder.Where("Id=@0",id);
            return FirstOrDefault<SysException>(sql);
        }
    }
}
