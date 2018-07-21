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

namespace MyProject.Matrix.Controllers.Api
{
    public class WxApiController : Controller
    {
        private readonly WeiXinSdkTask _sdk = new WeiXinSdkTask();
        private readonly LogTask _log = new LogTask();

        #region 公众号接收信息接口
        [HttpGet]
        [ActionName("Index")]
        public ActionResult Get(string signature, string timestamp, string nonce, string echostr)
        {
            var token = WeiXinSdkTask.Token;//微信公众平台后台设置的Token
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
            WeixinMessage message = null;
            var safeMode = Request.QueryString.Get("encrypt_type") == "aes";
            using (var streamReader = new StreamReader(Request.InputStream))
            {
                var decryptMsg = string.Empty;
                var msg = streamReader.ReadToEnd();

                #region 解密
                if (safeMode)
                {
                    var msg_signature = Request.QueryString.Get("msg_signature");
                    var wxBizMsgCrypt = new WXBizMsgCrypt(WeiXinSdkTask.Token, WeiXinSdkTask.appsecret, WeiXinSdkTask.appID);
                    var ret = wxBizMsgCrypt.DecryptMsg(msg_signature, timestamp, nonce, msg, ref decryptMsg);
                    if (ret != 0)//解密失败
                    {
                        _log.AddLog(new Log() { Msg = "message:" + ret + "request:" + msg,Ret=0,CreateTime=DateTime.Now });
                    }
                }
                else
                {
                    decryptMsg = msg;
                }
                #endregion

                message = AcceptMessageAPI.Parse(decryptMsg);
            }
            var response = _sdk.Execute(message);//处理接收到的信息
            var encryptMsg = string.Empty;
            #region 加密
            if (safeMode)
            {
                var msg_signature = Request.QueryString.Get("msg_signature");
                var wxBizMsgCrypt = new WXBizMsgCrypt(WeiXinSdkTask.Token, WeiXinSdkTask.appsecret, WeiXinSdkTask.appID);
                var ret = wxBizMsgCrypt.EncryptMsg(response, timestamp, nonce, ref encryptMsg);
                if (ret != 0)//加密失败
                {
                    SysLogTask.AddLog(new SysLogDto() { Message = "message:" + ret + "response:" + response, Module = LogModuleEnum.WeiXin, Operator = "zl", Result = "加密失败", Type = LogTypeEnum.WeiXinREceive });
                }
            }
            else
            {
                encryptMsg = response;
            }
            #endregion

            return new ContentResult
            {
                Content = encryptMsg,
                ContentType = "text/xml",
                ContentEncoding = System.Text.UTF8Encoding.UTF8
            };
        } 
        #endregion 

        /// <summary>
        /// 支付回调接口
        /// </summary>
        /// <returns></returns>
        public ActionResult PayNotify()
        {
            var msgBody = string.Empty;
            var stream = System.Web.HttpContext.Current.Request.InputStream;
            var b = new byte[stream.Length];
            stream.Read(b, 0, (int)stream.Length);
            msgBody = Encoding.UTF8.GetString(b);
            if (!string.IsNullOrWhiteSpace(msgBody))
            {
                var _payNotify = new WeiXinPayNotifySdk();
                var result = _payNotify.PayNotify(msgBody);
                _log.AddLog(new Log() { Msg = "结果:" + msgBody + "result:" + result.Msg, Ret = 0, CreateTime = DateTime.Now });
                return Content(result.Msg);
            }
            _log.AddLog(new Log() { Msg = "结果:null" , Ret = 0, CreateTime = DateTime.Now });
            return Content("");
        }

    }
}
