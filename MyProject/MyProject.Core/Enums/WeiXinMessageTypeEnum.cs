using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Core.Enums
{
    public enum WeiXinMessageTypeEnum
    {
        /// <summary>
        /// 文本
        /// </summary>
        [Description("文本")]
        text = 1,

        /// <summary>
        /// 图片
        /// </summary>
        [Description("图片")]
        image = 2,

        /// <summary>
        /// 语音
        /// </summary>
        [Description("语音")]
        voice = 3,

        /// <summary>
        /// 视频
        /// </summary>
        [Description("视频")]
        video = 4,

        /// <summary>
        /// 音乐
        /// </summary>
        [Description("音乐")]
        music = 5,

        /// <summary>
        /// 图文
        /// </summary>
        [Description("图文")]
        news = 6 
    }
}
