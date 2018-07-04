using MyProject.Services.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Core.Entities
{
    /// <summary>
    /// 回复信息表
    /// </summary>
    [TableName("WeiXinReplyMessage")]
    [PrimaryKey("Id")]
    public class WeiXinReplyMessage
    {
        /// <summary>
        /// id
        /// </summary>		 
        public int Id { get; set; }
        /// <summary>
        /// 触发类型
        /// </summary>
        public string ReplayType { get; set; }
        /// <summary>
        /// 回复信息类型
        /// </summary>		 
        public string MsgType { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>		 	 
        public string MatchKey { get; set; }
        /// <summary>
        /// 回复的消息内容
        /// </summary>		 
        public string Content { get; set; }
        /// <summary>
        /// 通过素材管理中的接口上传多媒体文件，得到的id
        /// </summary>		 
        public string MediaId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>		 
        public string Title { get; set; }
        /// <summary>
        /// 描述
        /// </summary>		 
        public string Description { get; set; }
        /// <summary>
        /// 音乐链接
        /// </summary>		 
        public string MusicURL { get; set; }
        /// <summary>
        /// 高质量音乐链接
        /// </summary>		 
        public string HQMusicUrl { get; set; }
        /// <summary>
        /// 缩略图的媒体id
        /// </summary>		 
        public string ThumbMediaId { get; set; }
        /// <summary>
        /// 图文消息个数，限制为8条以内
        /// </summary>		 
        public int ArticleCount { get; set; }
        /// <summary>
        /// Articles
        /// </summary>		 
        public string Articles { get; set; }
        /// <summary>
        /// 图片链接
        /// </summary>		 
        public string PicUrl { get; set; }
        /// <summary>
        /// 点击图文消息跳转链接
        /// </summary>		 
        public string Url { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>		 
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Creater
        /// </summary>		 
        public string Creater { get; set; }

        /// <summary>
        /// 用于测试，填写后就能够发送给这个openid下
        /// </summary>		 
        public string Openid { get; set; } 
    }
}
