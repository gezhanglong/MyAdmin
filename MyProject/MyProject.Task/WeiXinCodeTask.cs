using MyProject.Core.Entities;
using MyProject.Data.Daos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Task
{
    public class WeiXinCodeTask
    { 
        private readonly WeiXinCodeDao _dao = new WeiXinCodeDao();

        public WeiXinCode GetByUnionid(string unionid)
        {
           return   _dao.GetByUnionid(unionid);
        }

    }
}
