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
    public class SysLogDao:BaseDao<SysLog>
    {
        public SysLog GetSysLog(int id)
        {
            var sql = Sql.Builder.Where("Id=@0", id);
            return FirstOrDefault<SysLog>(sql);
        }

        public PagedList<SysLog> GetPagedList(string edate, string sdate, int logType, int logModule, int pageIndex, int pageSize)
        {
            var sql = Sql.Builder;
            if (logType != 0)
            {
                sql.Where("Type=@0",logType);
            }
            if (logModule != 0)
            {
                sql.Where("Module=@0", logModule);
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
                sql.Where("CreateTime>=@0 and CreateTime<@1", sdate,edate);
            }
            sql.OrderBy("Createtime desc");
            return PagedList<SysLog>(pageIndex,pageSize,sql);
        }
    }
}
