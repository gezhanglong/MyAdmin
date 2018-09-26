using System;
using MyProject.Services.ORM;

namespace MyProject.Core.Entities
{
    /// <summary>
    /// 调度器状态表
    /// </summary>
    [TableName("QRTZ_SCHEDULER_STATE")]
    [PrimaryKey("SCHED_NAME,INSTANCE_NAME")]
    public class QRTZ_SCHEDULER_STATE
    {
        
        /// <summary>
        /// 任务调度名称
        /// </summary>
        public string SCHED_NAME { get; set; }
        
        /// <summary>
        /// 配置文件中org.quartz.scheduler.instanceId配置的名字，如果设置为AUTO,quartz会根据物理机名和当前时间产生一个名字。
        /// </summary>
        public string INSTANCE_NAME { get; set; }
        
        /// <summary>
        /// 上次检入时间
        /// </summary>
        public long LAST_CHECKIN_TIME { get; set; }
        
        /// <summary>
        /// 检入间隔时间
        /// </summary>
        public long CHECKIN_INTERVAL { get; set; }
        
    }
}