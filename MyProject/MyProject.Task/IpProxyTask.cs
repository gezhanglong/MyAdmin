using MyProject.Core.Dtos;
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
    public class IpProxyTask
    {
        private readonly IpProxyDao _ipProxyDao = new IpProxyDao();
        private readonly RequestResultDto _result = new RequestResultDto() { Ret = -1, Msg = "" };

        public PagedList<IpProxy> GetPagedList(int pageIndex, int pageSize)
        {
            return _ipProxyDao.GetPagedList(pageIndex, pageSize);
        }
 
        public RequestResultDto DelIpProxy(string host)
        {
            try
            {
                _ipProxyDao.DelIpProxy(host);
                _result.Ret = 0;
                _result.Msg = "删除成功";
            }
            catch (Exception e)
            {
                _result.Msg = e.Message;
            }
            return _result;
        }

        public RequestResultDto UpdateIpProxy(IpProxy model)
        {
            try
            {
                _ipProxyDao.UpdateIpProxy(model);  
                _result.Ret = 0;
                _result.Msg = "修改成功";
            }
            catch (Exception e)
            {
                _result.Msg = e.Message;
            }
            return _result;
        }

        public RequestResultDto AddIpProxy(IpProxy model)
        {
            try
            {
                if (_ipProxyDao.GetIpProxy(model.Host) == null)
                {
                    _ipProxyDao.AddIpProxy(model);
                    _result.Ret = 0;
                    _result.Msg = "添加成功";
                    return _result;
                }
            }
            catch (Exception e)
            {
                _result.Msg = e.Message;
            }
            return _result;
        }
    }
}
