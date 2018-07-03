using System;

namespace MyProject.Controllers.SysManage.ViewModels
{
    public class MenuModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int MenuId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MenuName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LinkUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SortOrder { get; set; } 
    }
}