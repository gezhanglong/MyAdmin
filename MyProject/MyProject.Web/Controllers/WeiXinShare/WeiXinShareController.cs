﻿using Deepleo.Weixin.SDK;
using MyProject.Core.Dtos;
using MyProject.Core.Entities;
using MyProject.Core.Enum;
using MyProject.Services.Utility;
using MyProject.Task;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Web.Controllers.WeiXinShare
{
    public class WeiXinShareController : Controller
    {
        private readonly WeiXinSdkTask _sdk = new WeiXinSdkTask();
        private static readonly LogTask _log = new LogTask();
        public ActionResult Index()
        {
            var code = Request["code"];
            if (string.IsNullOrEmpty(code))//没有code表示授权失败
            {
                return Redirect(_sdk.OauthUrl(Server.UrlEncode("http://gxyzhanglong.eicp.net:40298/WeiXinShare/Index?r=" + Guid.NewGuid().ToString("N"))));
            }
            var token = _sdk.GetAccessToken(code, WeiXinSdkTask.appID, WeiXinSdkTask.appsecret); 
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
            //var dict = new Dictionary<string, string>  
            //{
            //    {"unionid","ok0Ues96SblI9MIhFC2FN1tjtXk41"},
            //};
            //var result = WebUtils.DoGet("http://m.lkgame.com/check/wxbind");
            return View();
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
