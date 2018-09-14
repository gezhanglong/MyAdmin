using MyProject.Core.Entities;
using MyProject.Data.Daos;
using MyProject.Services.MvcPager;
using MyProject.Services.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyProject.Task
{
    public class WeiXinMediaMessageTask
    {
        private readonly WeiXinMediaMessageDao _dao = new WeiXinMediaMessageDao();

        #region 后台
        public void AddMessage(WeiXinMediaMessage model)
        {
            _dao.Add(model);
        }

        public WeiXinMediaMessage GetById(int id)
        {
            return _dao.GetById(id);
        }

        public PagedList<WeiXinMediaMessage> GetPagedList(int pageIndex, int pageSize)
        {
            return _dao.GetPagedList(pageIndex,pageSize);
        }

        public List<WeiXinMediaMessage> GetList()
        {
            return _dao.GetList();
        }
        public void Add(WeiXinMediaMessage info)
        {
            _dao.Add(info);
        }

        public void Update(WeiXinMediaMessage info)
        {
            _dao.Update(info);
        }

        public void DeleteById(int menuId)
        {
            _dao.DeleteById(menuId);
        }
        #endregion 
    }
     
}
