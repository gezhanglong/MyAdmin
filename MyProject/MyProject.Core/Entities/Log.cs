using MyProject.Services.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Core.Entities
{
    /// <summary>
    /// 一般的日志
    /// </summary>
    [TableName("Log")]
    [PrimaryKey("Id")]
    public class Log
    {
        public int Id { get; set; }
        public string Msg { get; set; }
        public int Ret { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
