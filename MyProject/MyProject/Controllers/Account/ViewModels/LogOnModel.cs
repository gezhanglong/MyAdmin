using System.ComponentModel.DataAnnotations;

namespace MyProject.Controllers.Account.ViewModels
{
    public class LogOnModel
    {
        [Display(Name = "账 号"), Required]
        public string UserName { get; set; }

        [Display(Name = "密 码"), Required]
        public string Password { get; set; }

        [Display(Name = "验证码"), Required]
        public string ValidationCode { get; set; }
    }
}