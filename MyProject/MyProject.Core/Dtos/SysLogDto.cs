using MyProject.Core.Enum;
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
    public class SysLogDto
    { 
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
        public LogTypeEnum Type { get; set; }
        /// <summary>
        /// 操作模块
        /// </summary>
        public LogModuleEnum Module { get; set; } 
        
    }
}
