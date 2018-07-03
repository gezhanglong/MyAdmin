using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Core.Enums
{
    public enum WeiXinUserSubEnum
    {
        /// <summary>
        /// 关注
        /// </summary>
        [Description("关注")]
        sub = 1,
        /// <summary>
        /// 未关注
        /// </summary>
        [Description("未关注")]
        NoSub = 0
    }

    public enum WeiXinUserSexEnum
    {
        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        man = 1,
        /// <summary>
        /// 女
        /// </summary>
        [Description("女")]
        womem = 2,
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        no = 0
    }
}
