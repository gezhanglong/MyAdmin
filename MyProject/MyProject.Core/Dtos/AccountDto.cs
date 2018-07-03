using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Core.Dtos
{
    /// <summary>
    /// 登陆时session保存的数据
    /// </summary>
    public class AccountDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int AdminUserId { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public int RoleId { get; set; }
    }
}
