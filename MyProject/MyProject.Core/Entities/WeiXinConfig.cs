using System;
using MyProject.Services.ORM;

namespace MyProject.Core.Entities
{
    [TableName("WeiXinConfig")]
    [PrimaryKey("WeiXinId,AppId")]
    public class WeiXinConfig
    { 
        /// <summary>
        /// 
        /// </summary>
        public int Category { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string WeiXinId { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string AppId { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Appsecret { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string ApiUrl { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string ApiToken { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string WeiXinName { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string AccessToken { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string JsApiToken { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime? TokenUpdateTime { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string MchId { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string PartnerKey { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string CertUrl { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Creater { get; set; }
        
    }
}