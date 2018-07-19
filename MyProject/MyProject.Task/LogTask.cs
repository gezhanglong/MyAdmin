using MyProject.Core.Entities;
using MyProject.Data.Daos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Task
{
    public class LogTask
    {
        private readonly LogDao _log = new LogDao();

        public void AddLog(Log model)
        {
            _log.Add(model);
        }
    }
}
