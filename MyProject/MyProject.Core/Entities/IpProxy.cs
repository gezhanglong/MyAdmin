using MyProject.Services.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Core.Entities
{
    /// <summary>
    /// ip代理
    /// </summary>
    [TableName("IpProxy")]
    [PrimaryKey("Host")]
    public class IpProxy
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string Serve { get; set; }
        public string IsHide { get; set; }
        public string HttpType { get; set; }
        public DateTime FlushTime { get; set; }
        public DateTime CreateTime { get; set; } 
    }
}
