using System.ComponentModel.DataAnnotations;

namespace MyProject.Matrix.Controllers.Home.ViewModels
{
    public class LogOnModel
    {
        [Display(Name = "账 号"), Required(ErrorMessage = "账号不能为空")]
        public string UserName { get; set; }

        [Display(Name = "密 码"), Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }

        //[Display(Name = "验证码"), Required]
        //public string ValidationCode { get; set; }
    }
}