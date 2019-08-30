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
    public class IpProxyDao : BaseDao<IpProxy>
    {
        public PagedList<IpProxy> GetPagedList(int pageIndex, int pageSize)
        {
            var sql = Sql.Builder.Where("FlushTime<=@0",DateTime.Now.AddMinutes(-1));
            return PagedList<IpProxy>(pageIndex, pageSize, sql);
        }

        public IpProxy GetIpProxy(string host)
        {
            var sql = Sql.Builder.Where("Host=@0",host);
            return FirstOrDefault<IpProxy>(sql);
        }

        public void DelIpProxy(string host)
        {
            var sqlt = Sql.Builder.Append("delete IpProxy where Host=@0", host);
            Execute(sqlt);
        }

        public void AddIpProxy(IpProxy model)
        {
            var sql = Sql.Builder.Append("insert into IpProxy(Host,Port,Serve,IsHide,HttpType,FlushTime,CreateTime) values(@0,@1,@2,@3,@4,@5,@6)"
                ,model.Host,model.Port,model.Serve,model.IsHide,model.HttpType,model.FlushTime,model.CreateTime);
            Execute(sql);
        }


        public void UpdateIpProxy(IpProxy model)
        {
            var sqlt = Sql.Builder.Append("update IpProxy set serve=@1, FlushTime=getdate() where Host=@0", model.Host,model.Serve);
            Execute(sqlt);
        }
    }
}
