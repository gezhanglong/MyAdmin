using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Core.Enum
{
    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogTypeEnum
    {
        /// <summary>
        /// 登陆
        /// </summary>
        [Description("登陆")]
        Land=1,
        /// <summary>
        /// 退出
        /// </summary>
        [Description("退出")]
        SignOut = 2,
        /// <summary>
        /// 添加
        /// </summary>
        [Description("添加")]
        Add = 3,
        /// <summary>
        /// 修改
        /// </summary>
        [Description("修改")]
        Modification = 1,
        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Del = 4,
        /// <summary>
        /// 查看
        /// </summary>
        [Description("查看")]
        See = 5,
        /// <summary>
        /// 微信接收信息
        /// </summary>
        [Description("微信接收信息")]
        WeiXinREceive = 7
    }

    /// <summary>
    /// 日志模块
    /// </summary>
    public enum LogModuleEnum
    { 
        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        Other = 1,
        /// <summary>
        /// 登陆
        /// </summary>
        [Description("登陆")]
        Land = 2, 
        /// <summary>
        /// 系统管理
        /// </summary>
        [Description("系统管理")]
        Sys =3 ,
         /// <summary>
        /// 微信
        /// </summary>
        [Description("微信")]
        WeiXin =4 
    }
}
