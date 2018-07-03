using System.ComponentModel.DataAnnotations;

namespace MyProject.Matrix.Controllers.SysManage.ViewModels
{
    public class SaveMenuModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int? MenuId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "菜单名称"), Required]
        public string MenuName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "菜单链接")]
        public string LinkUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "父级菜单")]
        public int ParentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "排序编号")]
        public int SortOrder { get; set; }

        public string CreateDate { get; set; }
    }
}