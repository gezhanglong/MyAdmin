using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Matrix.Controllers.AdminUsers.ViewModels
{
    public class ChangePasswordModel
    {
        /// <summary>
        /// 旧密码
        /// </summary>
        [Display(Name = "旧密码"), Required(ErrorMessage = "请输入旧密码")]
        public string OldPw { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        [Display(Name = "新密码"), Required(ErrorMessage = "请输入新密码")]
        public string NewPw { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        [Display(Name = "确认密码"), Required(ErrorMessage = "请输入确认密码")]
        public string AgainPw { get; set; }

    }
}
