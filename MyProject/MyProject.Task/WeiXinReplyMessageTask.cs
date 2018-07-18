using MyProject.Core.Entities;
using MyProject.Core.Enums;
using MyProject.Data.Daos;
using MyProject.Services.MvcPager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Task
{
    public class WeiXinReplyMessageTask
    {
        private readonly WeiXinReplyMessageDao _dao = new WeiXinReplyMessageDao();

        #region 后台
        public void AddMessage(WeiXinReplyMessage model)
        {
            _dao.Add(model);
        }

        public WeiXinReplyMessage GetById(int id)
        {
            return _dao.GetById(id);
        }


        public PagedList<WeiXinReplyMessage> GetPagedList(int pageIndex, int pageSize)
        {
            return _dao.GetPagedList(pageIndex, pageSize);
        }

        public void Add(WeiXinReplyMessage info)
        {
            _dao.Add(info);
        }

        public void Update(WeiXinReplyMessage info)
        {
            _dao.Update(info);
        }

        public List<WeiXinReplyMessage> GetByReplayType()
        {
            return  _dao.GetByReplayType();
        }

        public void DeleteById(int menuId)
        {
            _dao.DeleteById(menuId);
        } 
        #endregion

        #region 前端
        public WeiXinReplyMessage GetMessage( string matchKey)
        {
            return _dao.GetMessage( matchKey);
        }
        public WeiXinReplyMessage GetMessageByReplayType(string replayType,string matchKey)
        {
            return _dao.GetMessageByReplayType(replayType,matchKey);
        }
        #endregion
    }
}
