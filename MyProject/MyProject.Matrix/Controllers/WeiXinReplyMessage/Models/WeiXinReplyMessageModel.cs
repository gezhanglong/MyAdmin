using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyProject.Matrix.Controllers.WeiXinReplyMessage.Models
{
    public class WeiXinReplyMessageModel
    {
        /// <summary>
        /// id
        /// </summary>	 
        public int? Id { get; set; }
        /// <summary>
        /// 触发类型
        /// </summary> 
        [Display(Name = "触发类型"), Required]
        public string ReplayType { get; set; }
        /// <summary>
        /// 回复信息类型
        /// </summary>		 
        [Display(Name = "信息类型"), Required]
        public string  MsgType { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>		 	 
        [Display(Name = "关键字"), Required]
        public string MatchKey { get; set; }

        /// <summary>
        /// 回复的消息内容
        /// </summary>		 	 	 
        [Display(Name = "消息内容")]
        public string Content { get; set; }
        /// <summary>
        /// 通过素材管理中的接口上传多媒体文件，得到的id
        /// </summary>		  	 	 
        [Display(Name = "媒体id")]
        public string MediaId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>		  	 	 
        [Display(Name = "标题")]
        public string Title { get; set; }
        /// <summary>
        /// 描述
        /// </summary>		  	 	 
        [Display(Name = "描述")]
        public string Description { get; set; }
        /// <summary>
        /// 音乐链接
        /// </summary>		  	 	 
        [Display(Name = "音乐链接")]
        public string MusicURL { get; set; }
        /// <summary>
        /// 高质量音乐链接
        /// </summary>		  	 	 
        [Display(Name = "高质量音乐链接")]
        public string HQMusicUrl { get; set; }
        /// <summary>
        /// 缩略图的媒体id
        /// </summary>		  	 	 
        [Display(Name = "缩略图的媒体id")]
        public string ThumbMediaId { get; set; }
        /// <summary>
        /// 图文消息个数，限制为8条以内
        /// </summary>		 	 	 
        [Display(Name = "图文消息个数")]
        public int ArticleCount { get; set; }
        /// <summary>
        /// Articles
        /// </summary>		 	 	 
        [Display(Name = "Articles")] 
        public string Articles { get; set; }
        /// <summary>
        /// 图片链接
        /// </summary>		 	 	 
        [Display(Name = "图片链接")] 
        public string PicUrl { get; set; }
        /// <summary>
        /// 图片链接1
        /// </summary>		 	 	 
        [Display(Name = "图片链接1")]
        public string PicUrl1 { get; set; }
        /// <summary>
        /// 图片链接2
        /// </summary>		 	 	 
        [Display(Name = "图片链接2")]
        public string PicUrl2 { get; set; }
        /// <summary>
        /// 图片链接3
        /// </summary>		 	 	 
        [Display(Name = "图片链接3")]
        public string PicUrl3 { get; set; }
        /// <summary>
        /// 图片链接4
        /// </summary>		 	 	 
        [Display(Name = "图片链接4")]
        public string PicUrl4 { get; set; }
        /// <summary>
        /// 图片链接5
        /// </summary>		 	 	 
        [Display(Name = "图片链接5")]
        public string PicUrl5 { get; set; }
        /// <summary>
        /// 图片链接6
        /// </summary>		 	 	 
        [Display(Name = "图片链接6")]
        public string PicUrl6 { get; set; }
        /// <summary>
        /// 图片链接7
        /// </summary>		 	 	 
        [Display(Name = "图片链接7")]
        public string PicUrl7 { get; set; } 
        /// <summary>
        /// 点击图文消息跳转链接
        /// </summary>		 	 	 
        [Display(Name = "跳转链接")] 
        public string Url { get; set; }
        /// <summary>
        /// 点击图文消息跳转链接1
        /// </summary>		 	 	 
        [Display(Name = "跳转链接1")]
        public string Url1 { get; set; }
        /// <summary>
        /// 点击图文消息跳转链接2
        /// </summary>		 	 	 
        [Display(Name = "跳转链接2")]
        public string Url2 { get; set; }
        /// <summary>
        /// 点击图文消息跳转链接3
        /// </summary>		 	 	 
        [Display(Name = "跳转链接3")]
        public string Url3 { get; set; }
        /// <summary>
        /// 点击图文消息跳转链接4
        /// </summary>		 	 	 
        [Display(Name = "跳转链接4")]
        public string Url4 { get; set; }
        /// <summary>
        /// 点击图文消息跳转链接5
        /// </summary>		 	 	 
        [Display(Name = "跳转链接5")]
        public string Url5 { get; set; }
        /// <summary>
        /// 点击图文消息跳转链接6
        /// </summary>		 	 	 
        [Display(Name = "跳转链接6")]
        public string Url6 { get; set; }
        /// <summary>
        /// 点击图文消息跳转链接7
        /// </summary>		 	 	 
        [Display(Name = "跳转链接7")]
        public string Url7 { get; set; }
        /// <summary>
        /// 标题1
        /// </summary>		  	 	 
        [Display(Name = "标题1")]
        public string Title1 { get; set; }
        /// <summary>
        /// 标题2
        /// </summary>		  	 	 
        [Display(Name = "标题2")]
        public string Title2 { get; set; }
        /// <summary>
        /// 标题3
        /// </summary>		  	 	 
        [Display(Name = "标题3")]
        public string Title3 { get; set; }
        /// <summary>
        /// 标题4
        /// </summary>		  	 	 
        [Display(Name = "标题4")]
        public string Title4 { get; set; }
        /// <summary>
        /// 标题5
        /// </summary>		  	 	 
        [Display(Name = "标题5")]
        public string Title5 { get; set; }
        /// <summary>
        /// 标题6
        /// </summary>		  	 	 
        [Display(Name = "标题6")]
        public string Title6 { get; set; }
        /// <summary>
        /// 标题7
        /// </summary>		  	 	 
        [Display(Name = "标题7")]
        public string Title7 { get; set; }
        /// <summary>
        /// 描述1
        /// </summary>		  	 	 
        [Display(Name = "描述1")]
        public string Description1 { get; set; }
        /// <summary>
        /// 描述2
        /// </summary>		  	 	 
        [Display(Name = "描述2")]
        public string Description2 { get; set; } 
        /// <summary>
        /// 描述3
        /// </summary>		  	 	 
        [Display(Name = "描述3")]
        public string Description3 { get; set; }
        /// <summary>
        /// 描述4
        /// </summary>		  	 	 
        [Display(Name = "描述4")]
        public string Description4 { get; set; }
        /// <summary>
        /// 描述5
        /// </summary>		  	 	 
        [Display(Name = "描述5")]
        public string Description5 { get; set; }
        /// <summary>
        /// 描述6
        /// </summary>		  	 	 
        [Display(Name = "描述6")]
        public string Description6 { get; set; }
        /// <summary>
        /// 描述7
        /// </summary>		  	 	 
        [Display(Name = "描述7")]
        public string Description7 { get; set; }

        /// <summary>
        /// openid
        /// </summary>	  	 	 
        [Display(Name = "Openid")]
        public string Openid { get; set; }
    }
}