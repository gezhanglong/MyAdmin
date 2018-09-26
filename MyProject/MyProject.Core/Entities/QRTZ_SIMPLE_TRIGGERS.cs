using System;
using MyProject.Services.ORM;

namespace MyProject.Core.Entities
{
    /// <summary>
    /// 简单的触发器详细信息
    /// </summary>
    [TableName("QRTZ_SIMPLE_TRIGGERS")]
    [PrimaryKey("SCHED_NAME,TRIGGER_NAME,TRIGGER_GROUP")]
    public class QRTZ_SIMPLE_TRIGGERS
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
        /// 重复次数 -1为无限
        /// </summary>
        public int REPEAT_COUNT { get; set; }
        
        /// <summary>
        /// 重复间隔
        /// </summary>
        public long REPEAT_INTERVAL { get; set; }
        
        /// <summary>
        /// 已触发次数
        /// </summary>
        public int TIMES_TRIGGERED { get; set; }
        
    }
}