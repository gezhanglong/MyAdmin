using MyProject.Services.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Core.Entities
{
    /// <summary>
    /// 日志
    /// </summary>
    [TableName("SysLog")]
    [PrimaryKey("Id")]
    public class SysLog
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 操作信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 操作结果
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 操作模块
        /// </summary>
        public int Module { get; set; } 
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
