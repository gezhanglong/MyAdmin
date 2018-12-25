using Deepleo.Weixin.SDK;
using MyProject.Controllers;
using MyProject.Core.Dtos;
using MyProject.Core.Entities;
using MyProject.Core.Enum;
using MyProject.Matrix.Controllers.WeiXinMediaMessage;
using MyProject.Services.Utility;
using MyProject.Task;
using Newtonsoft.Json;
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
    public class XiaoApiController : Controller
    {
        private readonly XiaoWeiXinSdkTask _sdk = new XiaoWeiXinSdkTask(); 

        [HttpGet]
        [ActionName("Index")]
        public ActionResult Get(string signature, string timestamp, string nonce, string echostr)
        {
            var token = XiaoWeiXinSdkTask.Token;//微信公众平台后台设置的Token
            if (string.IsNullOrEmpty(token)) return Content("请先设置Token！"); 
            if (!CheckSignature(XiaoWeiXinSdkTask.Token))
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

        /// <summary>
        /// 获取小程序账号信息（如果关注了公众号就会返回unionid,如果没关注过的不能用该方法拿unionid）
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ActionResult GetInfo(string code)
        {
            try
            {
                XiaoWeiXinAppDecryptTask _appDecrypt = new XiaoWeiXinAppDecryptTask(XiaoWeiXinSdkTask.appID, XiaoWeiXinSdkTask.appsecret);
                return Json(new RequestResultDto() { Msg = _appDecrypt.GetOpenIdAndSessionKeyString(code), Ret = 0 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                SysLogTask.AddLog(new SysLogDto() { Message = e.Message, Module = LogModuleEnum.WeiXin, Operator = "zl", Result = "加密失败", Type = LogTypeEnum.WeiXinREceive });

            }
            return Json(new RequestResultDto() { Msg = "错误", Ret = -1 }, JsonRequestBehavior.AllowGet);
        }

       /// <summary>
        /// 获取小程序账号信息（推荐使用这个，关注不关注都可以）
       /// </summary>
       /// <param name="loginInfo"></param>
       /// <returns></returns>
        public ActionResult GetInfo1(WechatLoginInfo loginInfo)
        {
            loginInfo.iv = "8lQK9zSsprNF4nt7EWr28g==";
            loginInfo.code = " 023nkGR901amAw1KKkV90nGzR90nkGRj";
            loginInfo.encryptedData = "P7Laqsb2NRvDXEc8A2c+6EVUHGdPkX3StB/waXPAkQSeZIK5Qu5L5xKQWgPoJ5nl+RZCZuxpKM6njeuFlfGV1uGTyPiMhOvOInioZI+xv+/Ufgxs7H/YnECc5oc5I78abmdpv/g/kerzUg9TW5tbiYG065HUlXJc+v1wPbhybKgU4PqLAHWpXtU7NPxn6Tsm89nQLJMm8c6QP3/uLckrJmJRMISKAPppuBROfVWDPYhGClsLf00t+SCLIsyVX2GPAOOgIYucXGXyd/9BsiguwkaNUiQRYm2sqwjIqvbWg5AY1Cxi7JWB09JcRJdjU7UG07Vk6juCMgCJ8CRn4XyjUpWfQ2tTP1DvS5bZ5yHcDX8KaZWkt4Is+WcG2kc2JR4exsfftYoDuZ23tRIgS6Ophl23In1epUIwIc65eRcRaCpiLemNt3RPjBnXC90EIKGTRTp1ogrA5teVz/s+TyxCIg==";
            XiaoWeiXinAppDecryptTask _appDecrypt = new XiaoWeiXinAppDecryptTask("wx32506441a78e116c", "c4cc7d0c56eaae726009bcec74d99ab5");
            return Json(new RequestResultDto() { Msg =JsonConvert.SerializeObject(_appDecrypt.Decrypt(loginInfo)), Ret = 0 }, JsonRequestBehavior.AllowGet);
        }


         //发送模板信息
        public void RepayTemplate()
        {
            _sdk.RepayTemplate("","","","","","","");
        }

         //发送图片信息
        public void RepayImage(string openId, string media_id)
        {
            _sdk.RepayImage(openId, media_id);
        }


        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        public ActionResult GetWxCode(string chanelid, string indirectchanel, string giftid, string unionid, int type = 0)
        {
            if (type == 0)//保存到本地
            {
                return Json(new RequestResultDto() { Msg = _sdk.GetWxCodeToLocal(unionid), Ret = 0 }, JsonRequestBehavior.AllowGet);
            }
            else//保存到数据库
            {
                return Json(new RequestResultDto() { Msg = _sdk.GetWxCodeToData(chanelid,indirectchanel,giftid,unionid,type), Ret = 0 }, JsonRequestBehavior.AllowGet);
            }
            
        }

        /// <summary>
        /// 显示二进制图片
        /// </summary>
        /// <param name="unionid"></param>
        /// <returns></returns>
        public ActionResult WxCodeIndex(string unionid)
        {
            return File(_sdk.WxCode(unionid), "image/jpg");//输出文件
        }


      
    }
}
