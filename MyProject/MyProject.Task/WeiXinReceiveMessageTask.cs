using MyProject.Core.Entities;
using MyProject.Data.Daos;
using MyProject.Services.MvcPager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Task
{
    public class WeiXinReceiveMessageTask
    {
        private readonly WeiXinReceiveMessageDao _dao = new WeiXinReceiveMessageDao();

        public void AddMessage(WeiXinReceiveMessage model)
        {
            _dao.Add(model);
        }

        public PagedList<WeiXinReceiveMessage> GetPagedList(string fields, string fieldValue, string sdate, string edate, string ContentValue, bool IsLike, int pageIndex, int pageSize)
        {
            return _dao.GetPagedList(fields,fieldValue,sdate,edate,ContentValue,IsLike, pageIndex, pageSize);
        }
    }
}
