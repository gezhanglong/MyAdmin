using MyProject.Services.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Core.Entities
{
    /// <summary>
    /// 系统异常错误记录表
    /// </summary>
    [TableName("SysException")]
    [PrimaryKey("Id")]
    public class SysException
    { 
        public int Id { get; set; }
         
        public string HelpLink { get; set; }
         
        public string Message { get; set; }
         
        public string Source { get; set; }
         
        public string StackTrace { get; set; }
         
        public string TargetSite { get; set; }
         
        public string Data { get; set; }
         
        public DateTime CreateTime { get; set; }
    }
}
