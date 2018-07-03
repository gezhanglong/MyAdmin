using System.ComponentModel.DataAnnotations;

namespace MyProject.Controllers.SysManage.ViewModels
{
    public class SavePowerModel
    { 
        /// <summary>
        /// 
        /// </summary>
       [Display(Name = "权限名称"),Required]
        public string PowerName { get; set; }

        /// <summary>
        /// 
        /// </summary>
       [Display(Name = "Action"), Required]
       public string Action { get; set; }

        /// <summary>
        /// 
        /// </summary>
       [Display(Name = "Controller"), Required]
       public string Controller { get; set; }

        /// <summary>
        /// 
        /// </summary>
       [Display(Name = "权限编码")]
       public string PowerCode { get; set; }

        /// <summary>
        /// 菜单ID
        /// </summary>
        public int MenuId { get; set; } 
    }
}