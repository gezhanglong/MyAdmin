using System;
using MyProject.Services.ORM;

namespace MyProject.Core.Entities
{
    /// <summary>
    /// 触发器信息表
    /// </summary>
    [TableName("QRTZ_TRIGGERS")]
    [PrimaryKey("SCHED_NAME,TRIGGER_NAME,TRIGGER_GROUP,JOB_NAME,JOB_GROUP")]
    public class QRTZ_TRIGGERS
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
        /// job名称
        /// </summary>
        public string JOB_NAME { get; set; }
        
        /// <summary>
        /// job所属组
        /// </summary>
        public string JOB_GROUP { get; set; }
        
        /// <summary>
        /// 描述
        /// </summary>
        public string DESCRIPTION { get; set; }
        
        /// <summary>
        /// 下次执行时间
        /// </summary>
        public long? NEXT_FIRE_TIME { get; set; }
        
        /// <summary>
        /// 上次执行时间
        /// </summary>
        public long? PREV_FIRE_TIME { get; set; }
        
        /// <summary>
        /// 优先级
        /// </summary>
        public int? PRIORITY { get; set; }
        
        /// <summary>
        /// 执行状态：WAITING，PAUSED，ACQUIRED分别为：等待，暂停，运行中
        /// </summary>
        public string TRIGGER_STATE { get; set; }
        
        /// <summary>
        /// 触发器类型：simple和cron
        /// </summary>
        public string TRIGGER_TYPE { get; set; }
        
        /// <summary>
        /// 开始执行时间
        /// </summary>
        public long START_TIME { get; set; }
        
        /// <summary>
        /// 结束执行时间
        /// </summary>
        public long? END_TIME { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string CALENDAR_NAME { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int? MISFIRE_INSTR { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public byte[] JOB_DATA { get; set; }
        
    }
}