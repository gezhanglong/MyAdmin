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
    public class WeiXinUserTask
    {
        private readonly WeiXinUserDao _dao = new WeiXinUserDao();

        public void AddUser(WeiXinUser model)
        {
            _dao.Add(model);
        }
        public WeiXinUser GetByOpenId(string openId)
        {
            return _dao.GetByOpenId(openId);
        }

        public PagedList<WeiXinUser> GetPagedList(string fields, string fieldValue, string sdate, string edate, bool IsLike, int pageIndex, int pageSize)
        {
            return _dao.GetPagedList(fields, fieldValue, sdate, edate, IsLike, pageIndex, pageSize);
        }
    }
}
