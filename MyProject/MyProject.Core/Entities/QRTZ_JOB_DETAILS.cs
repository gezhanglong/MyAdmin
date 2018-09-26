using System;
using MyProject.Services.ORM;

namespace MyProject.Core.Entities
{
    /// <summary>
    /// 存储每一个已配置的 Job 的详细信息 
    /// </summary>
    [TableName("QRTZ_JOB_DETAILS")]
    [PrimaryKey("SCHED_NAME,JOB_NAME,JOB_GROUP")]
    public class QRTZ_JOB_DETAILS
    {
         
        /// <summary>
        /// 任务调度名称
        /// </summary>
        public string SCHED_NAME { get; set; }
        
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
        ///  job实现类的完全包名 
        /// </summary>
        public string JOB_CLASS_NAME { get; set; }
        
        /// <summary>
        /// 是否持久化,把该属性设置为1，quartz会把job持久化到数据库中 
        /// </summary>
        public bool IS_DURABLE { get; set; }
        
        /// <summary>
        /// 是否集群
        /// </summary>
        public bool IS_NONCONCURRENT { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public bool IS_UPDATE_DATA { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public bool REQUESTS_RECOVERY { get; set; }
        
        /// <summary>
        /// 存放持久化job对象  
        /// </summary>
        public byte[] JOB_DATA { get; set; }
        
    }
}