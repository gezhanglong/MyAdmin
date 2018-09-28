using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyProject.Matrix.Controllers.Job.ViewModels
{
    public class TriggersModel
    { 

        /// <summary>
        /// 触发器名称
        /// </summary>
        [Display(Name = "触发器名称"), Required(ErrorMessage = "请填写触发器名称")]
        public string TRIGGER_NAME { get; set; }

        /// <summary>
        /// 触发器所属组
        /// </summary>
        [Display(Name = "触发器所属组"), Required(ErrorMessage = "请选择触发器所属组")]
        public string TRIGGER_GROUP { get; set; }

        /// <summary>
        /// job名称
        /// </summary>
        [Display(Name = "job名称"), Required(ErrorMessage = "请选择job")]
        public string JOB_NAME { get; set; }
         
        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = "描述")]
        public string DESCRIPTION { get; set; }
         

        /// <summary>
        /// 优先级
        /// </summary>
        [Display(Name = "优先级")]
        public int? PRIORITY { get; set; }

        /// <summary>
        /// 执行状态：WAITING，PAUSED，ACQUIRED分别为：等待，暂停，运行中
        /// </summary> 
        [Display(Name = "执行状态")]
        public string TRIGGER_STATE { get; set; }

        /// <summary>
        /// 触发器类型：simple和cron
        /// </summary>
        [Display(Name = "触发器类型")]
        public string TRIGGER_TYPE { get; set; }

        /// <summary>
        /// 开始执行时间
        /// </summary>
        [Display(Name = "开始执行时间"), Required(ErrorMessage = "请选择开始执行时间")]
        public DateTime  START_TIME { get; set; }

        /// <summary>
        /// 结束执行时间
        /// </summary>
        [Display(Name = "结束执行时间")]
        public DateTime? END_TIME { get; set; }

       

        /// <summary>
        /// 重复次数 -1为无限
        /// </summary> 
        [Display(Name = " 重复次数")]
        public int REPEAT_COUNT { get; set; }

        /// <summary>
        /// 重复间隔
        /// </summary>
        [Display(Name = "重复间隔")]
        public double REPEAT_INTERVAL { get; set; }
         

        /// <summary>
        ///  cron表达式
        /// </summary>
        [Display(Name = "cron表达式")]
        public string CRON_EXPRESSION { get; set; }
         

    }
}