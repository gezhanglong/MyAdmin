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
        private readonly WeiXinConfigTask _config = new WeiXinConfigTask();
        private readonly LogTask _log = new LogTask();

        #region 公众号接收信息接口
        [HttpGet]
        [ActionName("Index")]
        public ActionResult Get(string signature, string timestamp, string nonce, string echostr)
        {
            var appid = "";
            var appsecret = "";
            if (!CheckSignature(out appid, out appsecret))
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
                var appid = "";
                var appsecret = "";
                if (CheckSignature(out appid, out appsecret))
                {
                    WeixinMessage message = null;
                    string msgBody = "";
                    Stream s = System.Web.HttpContext.Current.Request.InputStream;
                    byte[] b = new byte[s.Length];
                    s.Read(b, 0, (int)s.Length);
                    msgBody = Encoding.UTF8.GetString(b);
                    if (string.IsNullOrWhiteSpace(msgBody))
                    {
                        _log.AddLog(new Log() { Msg = "lkpost过来的数据包：空" + msgBody.Length + DateTime.Now.ToString(), CreateTime = DateTime.Now, Ret = 0 });

                        return null;
                    }
                    _log.AddLog(new Log() { Msg = "msgBody:" + msgBody.Length, CreateTime = DateTime.Now, Ret = 0 });

                    message = AcceptMessageAPI.Parse(msgBody);
                    WeiXinSdkTask _sdk = new WeiXinSdkTask(appid, appsecret);
                    var response = _sdk.Execute(message);//处理接收到的信息
                    _log.AddLog(new Log() { Msg = "response:" + response, CreateTime = DateTime.Now, Ret = 0 });
                    return new ContentResult
                    {
                        Content = response,
                        ContentType = "text/xml",
                        ContentEncoding = System.Text.UTF8Encoding.UTF8
                    };

                }
                else
                {
                    _log.AddLog(new Log() { Msg = "lk消息真实性效验，不通过", CreateTime = DateTime.Now, Ret = 0 });

                }
            }
            catch (Exception ex)
            {
                _log.AddLog(new Log() { Msg = "lk出错：" + ex.Message + DateTime.Now.ToString(), CreateTime = DateTime.Now, Ret = 0 });

            }

            return Content(""); //返回空串表示有响应
        }

        public bool CheckSignature(out string appid, out string appsecret)
        {
            string signature = System.Web.HttpContext.Current.Request.QueryString["signature"] == null ? "" : System.Web.HttpContext.Current.Request.QueryString["signature"].Trim();
            string timestamp = System.Web.HttpContext.Current.Request.QueryString["timestamp"] == null ? "" : System.Web.HttpContext.Current.Request.QueryString["timestamp"].Trim();
            string nonce = System.Web.HttpContext.Current.Request.QueryString["nonce"] == null ? "" : System.Web.HttpContext.Current.Request.QueryString["nonce"].Trim();
            var configList = _config.GetListConfig();
            appid = "";
            appsecret = "";
            foreach (var item in configList)
            {
                string[] arrTmp = { item.ApiToken, timestamp, nonce };
                Array.Sort(arrTmp);
                string tmpStr = string.Join("", arrTmp);
                tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");//对该字符串进行sha1加密
                tmpStr = tmpStr.ToLower();//对字符串中的字母部分进行小写转换，非字母字符不作处理 
                if (tmpStr == signature) //开发者获得加密后的字符串可与signature对比
                {
                    _log.AddLog(new Log() { CreateTime = DateTime.Now, Ret = 0, Msg = "当前微信号:" + item.WeiXinId });
                    appid = item.AppId;
                    appsecret = item.Appsecret;
                    return true;
                }
            }

            return false;
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
            _log.AddLog(new Log() { Msg = "结果:null", Ret = 0, CreateTime = DateTime.Now });
            return Content("");
        }


        //发送小程序卡片信息
        public ActionResult RepayMiniprogrampage()
        {
            WeiXinSdkTask _sdk = new WeiXinSdkTask("wxfab14afc3ef13c1f", "fde7036077249329aece2f217677f206");
            return Content(_sdk.RepayMiniprogrampage("o55WfjpukwCuWWo1T91uE0jN_Fhc", "点击快速进入，只需观看视频即可领奖", "wxc016b052eb75755d", "", "SnL6aGPFiEqfVD5F5o3mKhVgNkhgDvwnc_EbadS08M4"));
        }
    }
}
