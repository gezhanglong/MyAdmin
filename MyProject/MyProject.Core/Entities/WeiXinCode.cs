using MyProject.Services.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Core.Entities
{
    /// <summary>
    /// 微信用户信息表
    /// </summary>
    [TableName("WeiXinCode")]
    [PrimaryKey("Id", autoIncrement = true)]
    public class WeiXinCode
    {
        public int Id { get; set; }
        public string  UnionId { get; set; }
        public string  WxCodeUrl { get; set; }
        public byte[] ImageStream { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
