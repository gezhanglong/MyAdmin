using System;

namespace MyProject.Matrix.Controllers.AdminUsers.ViewModels
{
    public class AdminUserModel
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int AdminUserId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool IsLock { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
    }
}
