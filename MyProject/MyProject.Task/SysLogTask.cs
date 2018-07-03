using MyProject.Core.Dtos;
using MyProject.Core.Entities;
using MyProject.Data.Daos;
using MyProject.Services.MvcPager;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Task
{
    public  class SysLogTask
    {
        private static  readonly SysLogDao _log = new SysLogDao();

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="model"></param>
        public static  void AddLog(SysLogDto model)
        {
            var data = new SysLog();
            data.CreateTime = DateTime.Now;
            data.Message = model.Message;
            data.Module = (int)model.Module;
            data.Operator = model.Operator;
            data.Type = (int)model.Type;
            data.Result = model.Result;
            _log.Add(data);
        }

        public PagedList<SysLog> GetPagedList(string edate, string sdate, int logType, int logModule, int pageIndex, int pageSize)
        {
            return _log.GetPagedList(edate,sdate,logType, logModule, pageIndex, pageSize);
        }

        public SysLog GetSysLog(int id)
        {
            return _log.GetSysLog(id);
        }
         
    }
}
