using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyProject.Matrix.Controllers.WeiXinConfig.ViewModels
{
    public class ConfigModel
    {
        public int Id { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        [Display(Name = "类别"), Required(ErrorMessage = "请选择类别")]
        public int Category { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "微信号"), Required(ErrorMessage = "请填写微信号")]
        public string WeiXinId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "appid"), Required(ErrorMessage = "请填写appid")]
        public string AppId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "appsecret"), Required(ErrorMessage = "请填写appsecret")]
        public string Appsecret { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "接口url")]
        public string ApiUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "接口token")]
        public string ApiToken { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "微信名称"), Required(ErrorMessage = "请填写微信名称")]
        public string WeiXinName { get; set; }
         
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "商户ID")]
        public string MchId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "商户key")]
        public string PartnerKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "证书地址")]
        public string CertUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }
        
    }
}