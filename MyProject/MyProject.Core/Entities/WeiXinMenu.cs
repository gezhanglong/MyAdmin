using MyProject.Services.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Core.Entities
{
    /// <summary>
    /// 微信菜单
    /// </summary>
    [TableName("WeiXinMenu")]
    //[PrimaryKey("menuid")]
    public class WeiXinMenu
    {
        public int menuid { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string key { get; set; }
        public string url { get; set; }
        public string media_id { get; set; }
        public string appid { get; set; }
        public string pagepath { get; set; }
        public DateTime createtime { get; set; }
        public string creater { get; set; } 
    }
}
