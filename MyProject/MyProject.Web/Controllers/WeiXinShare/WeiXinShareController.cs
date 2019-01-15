﻿using Deepleo.Weixin.SDK;
using MyProject.Core.Dtos;
using MyProject.Core.Entities;
using MyProject.Core.Enum;
using MyProject.Services.Utility;
using MyProject.Task;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Web.Controllers.WeiXinShare
{
    public class WeiXinShareController : Controller
    {
        private readonly WeiXinSdkTask _sdk = new WeiXinSdkTask("wx6d6715c94a2f0d19", "d4624c36b6795d1d99dcf0547af5443d");
        private static readonly LogTask _log = new LogTask();
        public ActionResult Index()
        {
            var code = Request["code"];
            if (string.IsNullOrEmpty(code))//没有code表示授权失败
            {
                return Redirect(_sdk.OauthUrl(Server.UrlEncode("http://gxyzhanglong.eicp.net:40298/WeiXinShare/Index?r=" + Guid.NewGuid().ToString("N"))));
            }
            var token = _sdk.GetAccessToken(code); 
            var userinfo = _sdk.GetUserInfo(token.access_token, token.openid); 
            ViewBag.WxNickName = userinfo.nickname;
            ViewBag.Score = 888;
            return View();
        }

        public ActionResult JsSDKIndex()
        {
            return View();
        }

        public ActionResult Test()
        {
            var unionid = "ok0Ues51PAh5nKaz53slvacjdD5E";
            var key = "lkwx7877787952213dddddeddss";
            var msg = "您好";
            var dict = new Dictionary<string, string>  
            {
                {"unionid",unionid},
                {"msg",msg},
                {"sign", CryptHelper.MD5Hash(msg+"_"+unionid+"_" + key).ToLower()},
            };
            // var ss = CryptHelper.MD5(unionid + key, 32).ToLower();
            // var result = WebUtils.DoPost("https://apiqa.lkgame.com/superlottery/sendgood?integralaward=1&integraltype=1&cashbalanceaward=1", dict);
            var result = WebUtils.DoPost("http://m.lkgame.com/api/sendtextcustom", dict);
            return View();
        }

        public ActionResult Test1()
        { 
            //var dict = new Dictionary<string, string>  
            //{
            //    {"wx_appid","wxe20f2a757ccbbce3"},
            //    {"openid", "oxC3qv_DpUKXo_ZvfI9BsFiidxu4"},
            //};
            //var result = WebUtils.DoPost("http://112.74.198.84:8110/get_one_pe_order_by_player", dict);
            var _WeiXinCodetask = new WeiXinCodeTask();
            var code = _WeiXinCodetask.GetByUnionid("oxC3qvz5VNrOms3Qbhv7nmKYeMu0");
            
            return File(code.ImageStream, "image/jpg");
        }

        public ActionResult Testsql(string openid)
        {
            var _user = new WeiXinUserTask();
           
            for (var i = 0; i < 1000; i++)
            {
                var user = _user.GetByOpenId(openid);
                _log.AddLog(new Log() { Ret = 2018, CreateTime = DateTime.Now, Msg = JsonConvert.SerializeObject(user) });
                
            }
            return Content("ok");
        } 
    }
}
