using MyProject.Services.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Core.Entities
{
    /// <summary>
    /// 接收微信信息
    /// </summary>
    [TableName("WeiXinReceiveMessage")]
    [PrimaryKey("Id")]
    public class WeiXinReceiveMessage
    {
        /// <summary>
        /// Id
        /// </summary>		 
        public int Id { get; set; }
        /// <summary>
        /// 开发者微信号
        /// </summary>		 
        public string ToUserName { get; set; }
        /// <summary>
        /// 发送方帐号（一个OpenID）
        /// </summary>		 
        public string FromUserName { get; set; }
        /// <summary>
        /// 消息创建时间
        /// </summary>		 
        public string CreateTime { get; set; }
        /// <summary>
        /// 接收消息类型
        /// </summary>		 
        public string MsgType { get; set; }
        /// <summary>
        /// 消息id
        /// </summary>		 
        public string MsgId { get; set; }
        /// <summary>
        /// 文本消息内容
        /// </summary>		 
        public string Content { get; set; }
        /// <summary>
        /// 图片链接
        /// </summary>		 
        public string PicUrl { get; set; }
        /// <summary>
        /// 媒体id
        /// </summary>		 
        public string MediaId { get; set; }
        /// <summary>
        /// 语音格式
        /// </summary>		 
        public string Format { get; set; }
        /// <summary>
        /// 语音识别结果，UTF8编码
        /// </summary>		 
        public string Recognition { get; set; }
        /// <summary>
        /// 视频消息缩略图的媒体id
        /// </summary>		 
        public string ThumbMediaId { get; set; }
        /// <summary>
        /// 地理位置维度
        /// </summary>		 
        public string Location_X { get; set; }
        /// <summary>
        /// 地理位置经度
        /// </summary>		 
        public string Location_Y { get; set; }
        /// <summary>
        /// 地图缩放大小
        /// </summary>		 
        public string Scale { get; set; }
        /// <summary>
        /// 地理位置信息
        /// </summary>		 
        public string Label { get; set; }
        /// <summary>
        /// 消息标题
        /// </summary>		 
        public string Title { get; set; }
        /// <summary>
        /// 消息描述
        /// </summary>		 
        public string Description { get; set; }
        /// <summary>
        /// 消息链接
        /// </summary>		 
        public string Url { get; set; }
        /// <summary>
        /// 事件类型
        /// </summary>		 
        public string EventType { get; set; }
        /// <summary>
        /// 事件KEY值
        /// </summary>		 
        public string EventKey { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>		 
        public DateTime CreateDate { get; set; }       
    }
}
