using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Core.Enums
{
    public enum WeiXinMediaMessageEnum
    {
        /// <summary>
        /// 图片
        /// </summary>
        [Description("图片")]
        image = 1,

        /// <summary>
        /// 语音
        /// </summary>
        [Description("语音")]
        voice = 2,

        /// <summary>
        /// 视频
        /// </summary>
        [Description("视频")]
        video = 3,

        /// <summary>
        /// 缩略图
        /// </summary>
        [Description("缩略图")]
        thumb = 4,
    }

    public enum WeiXinMediaTypeMessageEnum
    {
        /// <summary>
        /// 永久
        /// </summary>
        [Description("永久")]
        Forever = 0,

        /// <summary>
        /// 临时
        /// </summary>
        [Description("临时")]
        Temporary = 1,
    }
}
