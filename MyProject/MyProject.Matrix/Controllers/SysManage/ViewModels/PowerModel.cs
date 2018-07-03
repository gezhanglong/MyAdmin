using System;

namespace MyProject.Matrix.Controllers.SysManage.ViewModels
{
    public class PowerModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int PowerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PowerName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PowerCode { get; set; }

        /// <summary>
        /// 菜单ID
        /// </summary>
        public int MenuId { get; set; }
    }
}