using MyProject.Services.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Core.Entities
{
    /// <summary>
    /// 微信素材信息
    /// </summary>
    [TableName("WeiXinMediaMessage")]
    [PrimaryKey("Id")]
    public class WeiXinMediaMessage
    {
        /// <summary>
        /// Id
        /// </summary>		 
        public int Id { get; set; }
        /// <summary>
        /// 素材ID
        /// </summary>		 
        public string MediaId { get; set; }
        /// <summary>
        /// 素材标题
        /// </summary>		 
        public string MediaTitle { get; set; }
        /// <summary>
        /// 类型 分别有图片（image）、语音（voice）、视频（video）和缩略图（thumb）
        /// </summary>		 
        public string MediaType { get; set; }
        /// <summary>
        /// 是否永久素材（临时素材 3天）
        /// </summary>		 
        public int IsForever { get; set; }
        /// <summary>
        /// 新增的图片素材的图片URL（仅新增图片素材时会返回该字段）
        /// </summary>		 
        public string Url { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>		 
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Create
        /// </summary>		 
        public string Creater { get; set; }   
    }
}
