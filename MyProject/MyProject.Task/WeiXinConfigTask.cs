using MyProject.Core.Dtos;
using MyProject.Core.Entities;
using MyProject.Data.Daos;
using MyProject.Services.MvcPager;
using MyProject.Services.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Task
{
    public class WeiXinConfigTask
    {
        private readonly WeiXinConfigDao _config = new WeiXinConfigDao();
        private readonly RequestResultDto _result = new RequestResultDto() { Ret = -1, Msg = "" };

        #region   操作
        public PagedList<WeiXinConfig> GetPagedListConfig(string weixinId, int pageIndex, int pageSize)
        {
            return _config.GetPagedListConfig(weixinId, pageIndex, pageSize);
        }

        public List<WeiXinConfig> GetListConfig()
        {
            return _config.GetListConfig();
        }


        public WeiXinConfig GetConfig(string weixinId)
        {
            return _config.GetConfig(weixinId);
        }

        public RequestResultDto DelConfig(string weixinId)
        {
            try
            {
                _config.DelConfig(weixinId);
                _result.Ret = 0;
                _result.Msg = "删除成功";
            }
            catch (Exception e)
            {
                _result.Msg = e.Message;
            }
            return _result;

        }

        public RequestResultDto UpdateConfig(WeiXinConfig model)
        {
            try
            {
                _config.UpdateConfig(model);
                _result.Ret = 0;
                _result.Msg = "修改成功";
            }
            catch (Exception e)
            {
                _result.Msg = e.Message;
            }
            return _result;

        }
        public RequestResultDto UpdateToken(WeiXinConfig model)
        {
            try
            {
                _config.UpdateToken(model);
                _result.Ret = 0;
                _result.Msg = "更新成功";
            }
            catch (Exception e)
            {
                _result.Msg = e.Message;
            }
            return _result;

        }

        public RequestResultDto AddConfig(WeiXinConfig model)
        {
            try
            {
                _config.AddConfig(model);
                _result.Ret = 0;
                _result.Msg = "添加成功";
            }
            catch (Exception e)
            {
                _result.Msg = e.Message;
            }
            return _result;
        }

        public WeiXinConfig Check(string weiXinName, string appId, string apiToken)
        {
            return _config.Check(weiXinName,appId,apiToken);
        }
        #endregion
         
    }
}
