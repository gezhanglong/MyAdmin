using MyProject.Core.Entities;
using MyProject.Data.Daos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Task
{
    public class WeiXinMenuTask
    {
        private readonly WeiXinMenuDao _dao = new WeiXinMenuDao();

        public void Add(WeiXinMenu model)
        {
            _dao.Add(model);
        }

        public void Update(WeiXinMenu model)
        {
            _dao.Update(model);
        }

        public void Delete(int menuId)
        {
            _dao.Delete(menuId);
        }

        public WeiXinMenu GetById(int menuId)
        {
            return _dao.GetById(menuId);
        }

        public List<WeiXinMenu> GetList()
        {
            return _dao.GetList();
        }
    }
}
