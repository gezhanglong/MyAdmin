using MyProject.Services.Utility;
using MyProject.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Web.Controllers.ApiWeb
{
    public class ApiWebController : Controller
    {
        private readonly WeiXinSdkTask _sdk = new WeiXinSdkTask();

        public ActionResult Index()
        {
            return Content("sdfwet");
        }
        #region 公众号分享接口
        public ActionResult JsApiConfig(string url, bool debug = false  )
        {
            var obj = GetJsApiConfigModel(url, debug);
            // 允许跨域访问
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public JsApiConfigModel GetJsApiConfigModel(string url, bool debug = false  )
        {
            string[] jsApiList = new string[]
                                     {
                                         "onMenuShareTimeline",
                                         "onMenuShareAppMessage",
                                         "onMenuShareQQ",
                                         "onMenuShareWeibo",
                                         "startRecord",
                                         "stopRecord",
                                         "onVoiceRecordEnd",
                                         "playVoice",
                                         "pauseVoice",
                                         "stopVoice",
                                         "onVoicePlayEnd",
                                         "uploadVoice",
                                         "downloadVoice",
                                         "chooseImage",
                                         "previewImage",
                                         "uploadImage",
                                         "downloadImage",
                                         "translateVoice",
                                         "getNetworkType",
                                         "openLocation",
                                         "getLocation",
                                         "hideOptionMenu",
                                         "showOptionMenu",
                                         "hideAllNonBaseMenuItem",
                                         "showAllNonBaseMenuItem",
                                         "closeWindow",
                                         "scanQRCode",
                                         "chooseWXPay",
                                         "openProductSpecificView",
                                         "addCard",
                                         "chooseCard",
                                         "openCard"
                                     };
            var timestamp = Utils.GetTimeZ(DateTime.Now);
            var nonceStr = Utils.CreateNoncestr(16);
            var jsapiticket = _sdk.JsApiToken();
            var str1 = string.Format("jsapi_ticket={0}&noncestr={1}&timestamp={2}&url={3}", jsapiticket, nonceStr, timestamp, url);
            var signature = CryptHelper.SHA1Hash(str1).ToLower();
            var obj = new JsApiConfigModel
            {
                debug = debug,
                appId = WeiXinSdkTask.appID,
                timestamp = timestamp,
                nonceStr = nonceStr,
                jsApiList = jsApiList,
                signature = signature,
            };
            return obj;
        }
        #endregion
    }


    //分享configModel
    public class JsApiConfigModel
    {
        public bool debug { get; set; }
        public string appId { get; set; }
        public int timestamp { get; set; }
        public string nonceStr { get; set; }
        public string[] jsApiList { get; set; }
        public string signature { get; set; }
    }
}
