using System;
using MyProject.Services.ORM;

namespace MyProject.Core.Entities
{
    /// <summary>
    /// 执行中的触发器
    /// </summary>
    [TableName("QRTZ_FIRED_TRIGGERS")]
    [PrimaryKey("SCHED_NAME,ENTRY_ID")]
    public class QRTZ_FIRED_TRIGGERS
    {
        
        /// <summary>
        /// 
        /// </summary>
        public string SCHED_NAME { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string ENTRY_ID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string TRIGGER_NAME { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string TRIGGER_GROUP { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string INSTANCE_NAME { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public long FIRED_TIME { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public long SCHED_TIME { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int PRIORITY { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string STATE { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string JOB_NAME { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string JOB_GROUP { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public bool IS_NONCONCURRENT { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public bool REQUESTS_RECOVERY { get; set; }
        
    }
}