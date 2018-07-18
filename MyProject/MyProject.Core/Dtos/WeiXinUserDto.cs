using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Core.Dtos
{
    public class WeiXinUserDto
    {
        /// <summary>
        /// opendd
        /// </summary>		 
        public string openid { get; set; }
        /// <summary>
        /// subscribe
        /// </summary>		 
        public decimal subscribe { get; set; }
        /// <summary>
        /// nickname
        /// </summary>		 
        public string nickname { get; set; }
        /// <summary>
        /// sex
        /// </summary>		 
        public decimal  sex { get; set; }
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
        public decimal  subscribe_time { get; set; }
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
        public decimal  groupid { get; set; }

        /// <summary>
        /// subscribe_scene
        /// </summary>		 
        public string subscribe_scene { get; set; }
        /// <summary>
        /// qr_scene
        /// </summary>		 
        public decimal  qr_scene { get; set; }
        /// <summary>
        /// qr_scene_str
        /// </summary>		 
        public string qr_scene_str { get; set; }
    }
}
