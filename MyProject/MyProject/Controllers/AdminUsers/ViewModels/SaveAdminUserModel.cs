using System;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Controllers.AdminUsers.ViewModels
{
    public class SaveAdminUserModel
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int? AdminUserId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [Display(Name = "用户名称"), Required(ErrorMessage = "请输入用户名称")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "登录密码"), Required(ErrorMessage = "请输入登录密码")]
        public string Password { get; set; }

        /// <summary>
        /// 是否锁定
        /// </summary>
        [Display(Name = "是否锁定"), Required]
        public bool IsLock { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        [Display(Name = "用户角色")]
        public int RoleId { get; set; }
    }
}
