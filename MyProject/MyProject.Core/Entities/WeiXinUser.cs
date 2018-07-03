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
    [TableName("WeiXinUser")]
    [PrimaryKey("openid")]
    public class WeiXinUser
    {
        /// <summary>
        /// opendd
        /// </summary>		 
        public string openid { get; set; }
        /// <summary>
        /// subscribe
        /// </summary>		 
        public int subscribe { get; set; }
        /// <summary>
        /// nickname
        /// </summary>		 
        public string nickname { get; set; }
        /// <summary>
        /// sex
        /// </summary>		 
        public int sex { get; set; }
        /// <summary>
        /// language
        /// </summary>		 
        public string language { get; set; }
        /// <summary>
        /// city
        /// </summary>		 
        public string city { get; set; }
        /// <summary>
        /// province
        /// </summary>		 
        public string province { get; set; }
        /// <summary>
        /// country
        /// </summary>		 
        public string country { get; set; }
        /// <summary>
        /// headimgurl
        /// </summary>		 
        public string headimgurl { get; set; }
        /// <summary>
        /// subscribe_time
        /// </summary>		 
        public int subscribe_time { get; set; }
        /// <summary>
        /// unionid
        /// </summary>		 
        public string unionid { get; set; }
        /// <summary>
        /// remark
        /// </summary>		 
        public string remark { get; set; }
        /// <summary>
        /// groupid
        /// </summary>		 
        public int groupid { get; set; }
        /// <summary>
        /// tagid_list
        /// </summary>		 
        public string tagid_list { get; set; }
        /// <summary>
        /// subscribe_scene
        /// </summary>		 
        public string subscribe_scene { get; set; }
        /// <summary>
        /// qr_scene
        /// </summary>		 
        public int qr_scene { get; set; }
        /// <summary>
        /// qr_scene_str
        /// </summary>		 
        public string qr_scene_str { get; set; }

        public DateTime createtime { get; set; }
    }
}
