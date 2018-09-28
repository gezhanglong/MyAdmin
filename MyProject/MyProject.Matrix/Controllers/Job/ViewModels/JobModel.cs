using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyProject.Matrix.Controllers.Job.ViewModels
{
    public class JobModel
    {
        /// <summary>
        /// 任务调度名称
        /// </summary>
        [Display(Name = "任务调度名称"), Required(ErrorMessage = "请选择任务调度名称")]
        public string SCHED_NAME { get; set; }

        /// <summary>
        /// job名称
        /// </summary>
        [Display(Name = "job名称"), Required(ErrorMessage = "请输入job名称")]
        public string JOB_NAME { get; set; }

        /// <summary>
        /// job所属组
        /// </summary>
        [Display(Name = "job所属组"), Required(ErrorMessage = "请选择组名")]
        public string JOB_GROUP { get; set; }

        /// <summary>
        /// 描述
        /// </summary> 
        public string DESCRIPTION { get; set; }

        /// <summary>
        ///  job实现类的完全包名 
        /// </summary>
        [Display(Name = "job实现类"), Required(ErrorMessage = "请输入job实现类")]
        public string JOB_CLASS_NAME { get; set; }
    }
}