using Deepleo.Weixin.SDK;
using MyProject.Controllers;
using MyProject.Core.Entities;
using MyProject.Core.Enum;
using MyProject.Task;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Tencent;

namespace MyProject.Areas.WeiXin.Controllers.Api
{
    public class XiaoApiController : Controller
    {
        private readonly XiaoWeiXinSdkTask _sdk = new XiaoWeiXinSdkTask(); 

        [HttpGet]
        [ActionName("Index")]
        public ActionResult Get(string signature, string timestamp, string nonce, string echostr)
        {
            var token = XiaoWeiXinSdkTask.Token;//微信公众平台后台设置的Token
            if (string.IsNullOrEmpty(token)) return Content("请先设置Token！");
            var ent = ""; 
            if (!BasicAPI.CheckSignature(signature, timestamp, nonce, token, out ent))
            {
                return Content("参数错误！");
            }
            return Content(echostr); //返回随机字符串则表示验证通过
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult Post(string signature, string timestamp, string nonce, string echostr)
        {
            try
            {
                if (CheckSignature(XiaoWeiXinSdkTask.Token))
                {
                    WeixinMessage message =null;
                    string msgBody = "";
                    Stream s = System.Web.HttpContext.Current.Request.InputStream;
                    byte[] b = new byte[s.Length];
                    s.Read(b, 0, (int)s.Length);
                    msgBody = Encoding.UTF8.GetString(b);
                    if (string.IsNullOrWhiteSpace(msgBody))
                    {
                        SysLogTask.AddLog(new SysLogDto() { Message = "lkpost过来的数据包：空" + msgBody.Length + DateTime.Now.ToString(), Module = LogModuleEnum.WeiXin, Operator = "zl", Result = "加密失败", Type = LogTypeEnum.WeiXinREceive });
                        
                        return null;
                    }
                    SysLogTask.AddLog(new SysLogDto() { Message = "msgBody:" + msgBody.Length, Module = LogModuleEnum.WeiXin, Operator = "zl", Result = "加密失败", Type = LogTypeEnum.WeiXinREceive });
                    
                    message = AcceptMessageAPI.Parse(msgBody);
                    var response = _sdk.Execute(message);//处理接收到的信息
                    SysLogTask.AddLog(new SysLogDto() { Message = "response:" + response, Module = LogModuleEnum.WeiXin, Operator = "zl", Result = "加密失败", Type = LogTypeEnum.WeiXinREceive });
                    
                }
                else
                {
                    SysLogTask.AddLog(new SysLogDto() { Message = "lk消息真实性效验，不通过", Module = LogModuleEnum.WeiXin, Operator = "zl", Result = "加密失败", Type = LogTypeEnum.WeiXinREceive });
                    
                }
            }
            catch (Exception ex)
            {
                SysLogTask.AddLog(new SysLogDto() { Message = "lk出错：" + ex.Message + DateTime.Now.ToString(), Module = LogModuleEnum.WeiXin, Operator = "zl", Result = "加密失败", Type = LogTypeEnum.WeiXinREceive });
                     
            }

            return Content(""); //返回空串表示有响应
        }




        public bool CheckSignature(string token)
        {

            string signature = System.Web.HttpContext.Current.Request.QueryString["signature"] == null ? "" : System.Web.HttpContext.Current.Request.QueryString["signature"].Trim();
            string timestamp = System.Web.HttpContext.Current.Request.QueryString["timestamp"] == null ? "" : System.Web.HttpContext.Current.Request.QueryString["timestamp"].Trim();
            string nonce = System.Web.HttpContext.Current.Request.QueryString["nonce"] == null ? "" : System.Web.HttpContext.Current.Request.QueryString["nonce"].Trim();

            string[] arrTmp = { token, timestamp, nonce };
            Array.Sort(arrTmp);
            string tmpStr = string.Join("", arrTmp);
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");//对该字符串进行sha1加密
            tmpStr = tmpStr.ToLower();//对字符串中的字母部分进行小写转换，非字母字符不作处理
            return tmpStr == signature; //开发者获得加密后的字符串可与signature对比

        }
    }
}
