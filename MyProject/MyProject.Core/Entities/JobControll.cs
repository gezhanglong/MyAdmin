using System;
using MyProject.Services.ORM;

namespace MyProject.Core.Entities
{
    [TableName("JobControll")]
    [PrimaryKey("Id")]
    public class JobControll
    {
        
        /// <summary>
        /// 作业控制器
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        
        /// <summary>
        /// 作业下一个执行点
        /// </summary>
        public DateTime JobTime { get; set; }
        
        /// <summary>
        /// 时间间隔（根据jobspace和JobTime的时间差作为时间间隔）
        /// </summary>
        public int JobSpace { get; set; }
        
        /// <summary>
        /// 作业名称
        /// </summary>
        public string JobName { get; set; }
        
        /// <summary>
        /// 作业循环次数（0就是无限）
        /// </summary>
        public int JobNum { get; set; }
        
        /// <summary>
        /// 已经执行次数
        /// </summary>
        public int? JobNumAlready { get; set; }
        
        /// <summary>
        /// 报错信息
        /// </summary>
        public string JobError { get; set; }

        /// <summary>
        /// 是否正在运行
        /// </summary>
        public bool IsTimeing { get; set; }


        /// <summary>
        /// 是否开启
        /// </summary>
        public bool IsOpen { get; set; }
        
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creater { get; set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        
    }
}