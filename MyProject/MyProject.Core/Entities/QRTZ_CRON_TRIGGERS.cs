using MyProject.Services.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Core.Entities
{
    /// <summary>
    /// cron触发器详细信息
    /// </summary>
    [TableName("QRTZ_CRON_TRIGGERS")]
    [PrimaryKey("SCHED_NAME,TRIGGER_NAME,TRIGGER_GROUP")]
    public class QRTZ_CRON_TRIGGERS
    {
        /// <summary>
        /// 任务调度名称
        /// </summary>
        public string SCHED_NAME { get; set; }

        /// <summary>
        /// 触发器名称
        /// </summary>
        public string TRIGGER_NAME { get; set; }

        /// <summary>
        /// 触发器所属组
        /// </summary>
        public string TRIGGER_GROUP { get; set; }

        /// <summary>
        /// cron表达式
        /// </summary>
        public string  CRON_EXPRESSION { get; set; }

        /// <summary>
        /// 重复间隔
        /// </summary>
        public string  TIME_ZONE_ID { get; set; }

       
    }
}
