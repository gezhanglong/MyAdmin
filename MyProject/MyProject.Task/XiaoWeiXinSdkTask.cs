using Deepleo.Weixin.SDK;
using Deepleo.Weixin.SDK.Entities;
using MyProject.Core.Entities;
using MyProject.Core.Enum;
using MyProject.Core.Enums;
using MyProject.Data.Daos;
using MyProject.Services.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyProject.Task
{
    /// <summary>
    /// 有关微信的操作都放在这个类
    /// </summary>
    public class XiaoWeiXinSdkTask
    {
        public static string appID = "wx8f88be576c5d04ed";
        public static string appsecret = "e094d445c87ad7f45d82e10325ff3697";
        public static string Token = "XiaoChengXulkgame";
        public static string domain = ResultHelper.GetBaseUrl();//域名
        private readonly WeiXinReceiveMessageTask _receiveMessage = new WeiXinReceiveMessageTask();

        /// <summary>
        /// 说明：带TODO字眼的代码段，需要开发者自行按照自己的业务逻辑实现
        /// </summary>
        /// <param name="message"></param>
        /// <returns>已经打包成xml的用于回复用户的消息包</returns>
        public string Execute(WeixinMessage message)
        {
            var result = "";
            var openId = message.Body.FromUserName.Value;
            var myUserName = message.Body.ToUserName.Value;
            //这里需要调用TokenHelper获取Token的，省略了。 

            #region 记录接收信息
            var receiveMessage = new WeiXinReceiveMessage()
               {
                   ToUserName = myUserName,
                   FromUserName = openId,
                   CreateDate = DateTime.Now,
                   CreateTime = message.Body.CreateTime.Value,
                   MsgType = message.Body.MsgType.Value,
                   MsgId = message.Type == WeixinMessageType.Event ? "" : message.Body.MsgId.Value //事件没有这个参数
               };
            #endregion
            switch (message.Type)
            {
                case WeixinMessageType.Text://文字消息
                    #region 文字消息
                    string userMessage = message.Body.Content.Value;
                    receiveMessage.Content = userMessage;//用于记录接收信息 
                    switch (userMessage)
                    {
                        case "1":
                            result = RepayText(openId, "抓娃娃<a href='https://zww-wechat-daili-callback.lkgame.com/wawa_apph5/index.html?env=prd'>点击跳转</a>");
                            break;
                        case "2":
                            result = RepayLink(openId, "抓娃娃", "超级好玩的娃娃机", "https://zww-wechat-daili-callback.lkgame.com/wawa_apph5/index.html?env=prd", "http://5acbaf99.ngrok.io/Content/images/Xiao_1280.jpg");
                            break;
                        default:
                            result = RepayText(openId, "客官，小二无言以对！！");
                            break;
                    }
                    #endregion
                    break;
                case WeixinMessageType.Image://图片消息
                    #region 图片消息
                    string imageUrl = message.Body.PicUrl.Value;//图片地址
                    string mediaId = message.Body.MediaId.Value;//mediaId
                    receiveMessage.PicUrl = imageUrl;
                    receiveMessage.MediaId = mediaId;
                    #endregion
                    break;
                case WeixinMessageType.Event:
                    #region 事件
                    string eventType = message.Body.Event.Value.ToLower();
                    string eventKey = string.Empty;
                    try
                    {
                        eventKey = message.Body.EventKey.Value;
                    }
                    catch { }
                    receiveMessage.EventType = eventType;
                    receiveMessage.EventKey = eventKey;
                    switch (eventType)
                    {
                        case "subscribe"://用户未关注时，进行关注后的事件推送
                            #region 首次关注

                            //TODO: 获取用户基本信息后，将用户信息存储在本地。
                            //var weixinInfo = UserAdminAPI.GetInfo(token, openId);//注意：订阅号没有此权限

                            if (!string.IsNullOrEmpty(eventKey))
                            {
                                var qrscene = eventKey.Replace("qrscene_", "");//此为场景二维码的场景值
                                result = ReplayPassiveMessageAPI.RepayNews(openId, myUserName,
                                    new WeixinNews
                                    {
                                        title = "欢迎订阅，场景值：" + qrscene,
                                        description = "欢迎订阅，场景值：" + qrscene,
                                        picurl = string.Format("{0}/ad.jpg", domain),
                                        url = domain
                                    });
                            }
                            else
                            {
                                result = ReplayPassiveMessageAPI.RepayNews(openId, myUserName,
                                 new WeixinNews
                                 {
                                     title = "欢迎订阅",
                                     description = "欢迎订阅，点击此消息查看在线demo",
                                     picurl = string.Format("{0}/ad.jpg", domain),
                                     url = domain
                                 });
                            }
                            #endregion
                            break;
                        case "unsubscribe"://取消关注
                            #region 取消关注
                            result = ReplayPassiveMessageAPI.RepayText(openId, myUserName, "欢迎再来");
                            #endregion
                            break;
                        case "scan":// 用户已关注时的事件推送
                            #region 已关注扫码事件
                            if (!string.IsNullOrEmpty(eventKey))
                            {
                                var qrscene = eventKey.Replace("qrscene_", "");//此为场景二维码的场景值
                                result = ReplayPassiveMessageAPI.RepayNews(openId, myUserName,
                                    new WeixinNews
                                    {
                                        title = "欢迎使用，场景值：" + qrscene,
                                        description = "欢迎使用，场景值：" + qrscene,
                                        picurl = string.Format("{0}/ad.jpg", domain),
                                        url = domain
                                    });
                            }
                            else
                            {
                                result = ReplayPassiveMessageAPI.RepayNews(openId, myUserName,
                                 new WeixinNews
                                 {
                                     title = "欢迎使用",
                                     description = "欢迎订阅，点击此消息查看在线demo",
                                     picurl = string.Format("{0}/ad.jpg", domain),
                                     url = domain
                                 });
                            }
                            #endregion
                            break;
                        case "masssendjobfinish"://事件推送群发结果,
                            #region 事件推送群发结果
                            {
                                var msgId = message.Body.MsgID.Value;
                                var msgStatus = message.Body.Status.Value;//“send success”或“send fail”或“err(num)” 
                                //send success时，也有可能因用户拒收公众号的消息、系统错误等原因造成少量用户接收失败。
                                //err(num)是审核失败的具体原因，可能的情况如下：err(10001)涉嫌广告, err(20001)涉嫌政治, err(20004)涉嫌社会, err(20002)涉嫌色情, err(20006)涉嫌违法犯罪,
                                //err(20008)涉嫌欺诈, err(20013)涉嫌版权, err(22000)涉嫌互推(互相宣传), err(21000)涉嫌其他
                                var totalCount = message.Body.TotalCount.Value;//group_id下粉丝数；或者openid_list中的粉丝数
                                var filterCount = message.Body.FilterCount.Value;//过滤（过滤是指特定地区、性别的过滤、用户设置拒收的过滤，用户接收已超4条的过滤）后，准备发送的粉丝数，原则上，FilterCount = SentCount + ErrorCount
                                var sentCount = message.Body.SentCount.Value;//发送成功的粉丝数
                                var errorCount = message.Body.FilterCount.Value;//发送失败的粉丝数
                                //TODO:开发者自己的处理逻辑,这里用log4net记录日志
                                // LogWriter.Default.WriteInfo(string.Format("mass send job finishe,msgId:{0},msgStatus:{1},totalCount:{2},filterCount:{3},sentCount:{4},errorCount:{5}", msgId, msgStatus, totalCount, filterCount, sentCount, errorCount));
                            }
                            #endregion
                            break;
                        case "templatesendjobfinish"://模版消息结果,
                            #region 模版消息结果
                            {
                                var msgId = message.Body.MsgID.Value;
                                var msgStatus = message.Body.Status.Value;//发送状态为成功: success; 用户拒绝接收:failed:user block; 发送状态为发送失败（非用户拒绝）:failed: system failed
                                //TODO:开发者自己的处理逻辑,这里用log4net记录日志
                                //LogWriter.Default.WriteInfo(string.Format("template send job finish,msgId:{0},msgStatus:{1}", msgId, msgStatus));
                            }
                            #endregion
                            break;
                        case "location"://上报地理位置事件
                            #region 上报地理位置事件
                            var lat = message.Body.Latitude.Value.ToString();
                            var lng = message.Body.Longitude.Value.ToString();
                            var pcn = message.Body.Precision.Value.ToString();
                            //TODO:在此处将经纬度记录在数据库,这里用log4net记录日志
                            // LogWriter.Default.WriteInfo(string.Format("openid:{0} ,location,lat:{1},lng:{2},pcn:{3}", openId, lat, lng, pcn));
                            #endregion
                            break;
                        case "voice"://语音消息
                            #region 语音消息
                            //A：已开通语音识别权限的公众号
                            var userVoice = message.Body.Recognition.Value;//用户语音消息文字
                            result = ReplayPassiveMessageAPI.RepayText(openId, myUserName, "您说:" + userVoice);

                            //B：未开通语音识别权限的公众号
                            var userVoiceMediaId = message.Body.MediaId.Value;//media_id
                            //TODO:调用自定义的语音识别程序识别用户语义

                            #endregion
                            break;
                        case "image"://图片消息
                            #region 图片消息
                            var userImage = message.Body.PicUrl.Value;//用户语音消息文字
                            result = ReplayPassiveMessageAPI.RepayNews(openId, myUserName, new WeixinNews
                            {
                                title = "您刚才发送了图片消息",
                                picurl = string.Format("{0}/Images/ad.jpg", domain),
                                description = "点击查看图片",
                                url = userImage
                            });
                            #endregion
                            break;
                        case "click"://自定义菜单事件
                            #region 自定义菜单事件
                            {
                                switch (eventKey)
                                {
                                    case "myaccount"://CLICK类型事件举例
                                        #region 我的账户
                                        result = ReplayPassiveMessageAPI.RepayNews(openId, myUserName, new List<WeixinNews>()
                                    {
                                        new WeixinNews{
                                            title="我的帐户",
                                            url=string.Format("{0}/user?openId={1}",domain,openId),
                                            description="点击查看帐户详情",
                                            picurl=string.Format("{0}/Images/ad.jpg",domain)
                                        },
                                    });
                                        #endregion
                                        break;
                                    case "www.weixinsdk.net"://VIEW类型事件举例，注意：点击菜单弹出子菜单，不会产生上报。
                                        //TODO:后台处理逻辑
                                        break;
                                    default:
                                        result = ReplayPassiveMessageAPI.RepayText(openId, myUserName, "没有响应菜单事件");
                                        break;
                                }
                            }
                            #endregion
                            break;
                        case "view"://点击菜单跳转链接时的事件推送
                            #region 点击菜单跳转链接时的事件推送
                            result = ReplayPassiveMessageAPI.RepayText(openId, myUserName, string.Format("您将跳转至：{0}", eventKey));
                            #endregion
                            break;
                        case "scancode_push"://扫码推事件的事件推送
                            {
                                var scanType = message.Body.ScanCodeInfo.ScanType.Value;//扫描类型，一般是qrcode
                                var scanResult = message.Body.ScanCodeInfo.ScanResult.Value;//扫描结果，即二维码对应的字符串信息
                                result = ReplayPassiveMessageAPI.RepayText(openId, myUserName, string.Format("您扫描了二维码,scanType：{0},scanResult:{1},EventKey:{2}", scanType, scanResult, eventKey));
                            }
                            break;
                        case "scancode_waitmsg"://扫码推事件且弹出“消息接收中”提示框的事件推送
                            {
                                var scanType = message.Body.ScanCodeInfo.ScanType.Value;//扫描类型，一般是qrcode
                                var scanResult = message.Body.ScanCodeInfo.ScanResult.Value;//扫描结果，即二维码对应的字符串信息
                                result = ReplayPassiveMessageAPI.RepayText(openId, myUserName, string.Format("您扫描了二维码,scanType：{0},scanResult:{1},EventKey:{2}", scanType, scanResult, eventKey));
                            }
                            break;
                        case "pic_sysphoto"://弹出系统拍照发图的事件推送
                            {
                                var count = message.Body.SendPicsInfo.Count;//发送的图片数量
                                var picList = message.Body.PicList;//发送的图片信息
                                result = ReplayPassiveMessageAPI.RepayText(openId, myUserName, string.Format("弹出系统拍照发图,count：{0},EventKey:{1}", count, eventKey));
                            }
                            break;
                        case "pic_photo_or_album"://弹出拍照或者相册发图的事件推送
                            {
                                var count = message.Body.SendPicsInfo.Count.Value;//发送的图片数量
                                var picList = message.Body.PicList.Value;//发送的图片信息
                                result = ReplayPassiveMessageAPI.RepayText(openId, myUserName, string.Format("弹出拍照或者相册发图,count：{0},EventKey:{1}", count, eventKey));
                            }
                            break;
                        case "pic_weixin"://弹出微信相册发图器的事件推送
                            {
                                var count = message.Body.SendPicsInfo.Count.Value;//发送的图片数量
                                var picList = message.Body.PicList.Value;//发送的图片信息
                                result = ReplayPassiveMessageAPI.RepayText(openId, myUserName, string.Format("弹出微信相册发图器,count：{0},EventKey:{1}", count, eventKey));
                            }
                            break;
                        case "location_select"://弹出地理位置选择器的事件推送
                            {
                                var location_X = message.Body.SendLocationInfo.Location_X.Value;//X坐标信息
                                var location_Y = message.Body.SendLocationInfo.Location_Y.Value;//Y坐标信息
                                var scale = message.Body.SendLocationInfo.Scale.Value;//精度，可理解为精度或者比例尺、越精细的话 scale越高
                                var label = message.Body.SendLocationInfo.Label.Value;//地理位置的字符串信息
                                var poiname = message.Body.SendLocationInfo.Poiname.Value;//朋友圈POI的名字，可能为空  
                                result = ReplayPassiveMessageAPI.RepayText(openId, myUserName, string.Format("弹出地理位置选择器,location_X：{0},location_Y:{1},scale:{2},label:{3},poiname:{4},eventKey:{5}", location_X, location_Y, scale, label, poiname, eventKey));
                            }
                            break;
                        case "card_pass_check"://生成的卡券通过审核时，微信会把这个事件推送到开发者填写的URL。
                            {
                                var cardid = message.Body.CardId.Value;//CardId
                                result = ReplayPassiveMessageAPI.RepayText(openId, myUserName, string.Format("您的卡券已经通过审核"));
                            }
                            break;
                        case "card_not_pass_check"://生成的卡券未通过审核时，微信会把这个事件推送到开发者填写的URL。
                            {
                                var cardid = message.Body.CardId.Value;//CardId

                            }
                            break;
                        case "user_get_card"://用户在领取卡券时，微信会把这个事件推送到开发者填写的URL。
                            {
                                var cardid = message.Body.CardId.Value;//CardId
                                var isGiveByFriend = message.Body.IsGiveByFriend.Value;//是否为转赠，1代表是，0代表否。
                                var fromUserName = message.Body.FromUserName.Value;//领券方帐号（一个OpenID）
                                var friendUserName = message.Body.FriendUserName.Value;//赠送方账号（一个OpenID），"IsGiveByFriend”为1时填写该参数。
                                var userCardCode = message.Body.UserCardCode.Value;//code序列号。自定义code及非自定义code的卡券被领取后都支持事件推送。
                                var outerId = message.Body.OuterId.Value;//领取场景值，用于领取渠道数据统计。可在生成二维码接口及添加JSAPI接口中自定义该字段的整型值。

                            }
                            break;
                        case "user_del_card"://用户在删除卡券时，微信会把这个事件推送到开发者填写的URL
                            {
                                var cardid = message.Body.CardId.Value;//CardId
                                var userCardCode = message.Body.UserCardCode.Value;//商户自定义code值。非自定code推送为空
                            }
                            break;
                        case "merchant_order"://微信小店：订单付款通知:在用户在微信中付款成功后，微信服务器会将订单付款通知推送到开发者在公众平台网站中设置的回调URL（在开发模式中设置）中，如未设置回调URL，则获取不到该事件推送。
                            {
                                var orderId = message.Body.OrderId.Value;//CardId
                                var orderStatus = message.Body.OrderStatus.Value;//OrderStatus
                                var productId = message.Body.ProductId.Value;//ProductId
                                var skuInfo = message.Body.SkuInfo.Value;//SkuInfo

                            }
                            break;
                    }
                    #endregion
                    break;
                default:
                    result = RepayText(openId, string.Format("未处理消息类型:{0}", message.Type));
                    break;
            }
            _receiveMessage.AddMessage(receiveMessage);
            return result;
        }

        /// <summary>
        /// 获取accountToken并缓存1小时
        /// </summary>
        /// <returns></returns>
        public string AccountToken()
        {
            var token = "";
            try
            {
                token = CacheHelper.Get("XiaoAccountToken") as string;
                if (string.IsNullOrEmpty(token))
                {
                    token = BasicAPI.GetAccessToken(appID, appsecret).access_token;
                    CacheHelper.Set("XiaoAccountToken", token, 60 * 60);//缓存一个小时 
                }
            }
            catch (Exception e)
            {
                SysExceptionTask.AddException(e, "获取XiaoAccountToken");
            }
            return token;
        }

        //发送文本信息
        public string RepayText(string openId, string content)
        {
            return WebUtils.DoPost("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + AccountToken(), "{\"touser\":\"" + openId + "\",\"msgtype\":\"text\",\"text\":{\"content\":\"" + content + "\"}}");

        }

        //发送图文信息
        public string RepayLink(string openId, string title, string description, string url, string thumb_url)
        {
            return WebUtils.DoPost("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + AccountToken(), "{\"touser\":\"" + openId + "\",\"msgtype\":\"link\",\"link\":{\"title\":\"" + title + "\",\"description\":\"" + description + "\",\"url\":\"" + url + "\",\"thumb_url\":\"" + thumb_url + "\"}}");

        }

        //发送图片信息
        public string RepayImage(string openId, string media_id)
        {
            var AccessToken = "13_bvwIxfrllfagw2Q0zxi2f6W4zyHIKzJlo7uvCuimGp9UzZEVnBaSGjFSpWdrj4p3gA6SskL4fdgc54yj0KLrEgcuZJSvhCQSgWaysM1PVtrv5CHjzai9G8-wDXxXgcTfyngANDGzOXBog1wHWINdABAIUZ";
            openId = "oSYTi5Mw8P51KIkCo_D_QIYHzWl8";
            media_id = "sHRVZainI-WeLcvWhEiITB0-Vik6O13PUKZzea3vVP3mAt7J9uNgr6Y0irGJbY-N";
            return WebUtils.DoPost("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + AccessToken, "{\"touser\":\"" + openId + "\",\"msgtype\":\"image\",\"image\":{\"media_id\":\"" + media_id + "\"}}");

        }

        //发送模板信息
        public void RepayTemplate(string openId, string page, string name, string date, string result, string formid, string templateid)
        {
            var AccessToken = "13_Kwwo-LITiv-padZsdHFD1My87jSTdzndzaJ3ry3b0d3KA_3jxuph5nU83mAJ4yrE9zAZupANJyomnKgyj1encMSV6lgN1seQx3GKv_jNggoux-wLKMqeRahVjNRmt0CZ5O4l99CWUQoK6muhECGgABAOMQ";
            openId = "oSYTi5Mw8P51KIkCo_D_QIYHzWl8";
            page = "pages/index/index";
            name = "老铁，好久没来抽奖，想你了";
            date = "2018-8-28";
            result = "从哪里来";
            formid = "1535427217082";
            templateid = "GAAP6ubRmWbzUJgl7einBJaPJCUb2AvCbtebTDyggJ0";
            var str = WebUtils.DoPost("https://api.weixin.qq.com/cgi-bin/message/wxopen/template/send?access_token=" + AccessToken, "{\"touser\":\"" + openId + "\",\"template_id\":\"" + templateid + "\",\"page\":\"" + page + "\",\"form_id\":\"" + formid + "\",\"data\": {\"keyword1\":{\"value\":\"" + name + "\"},\"keyword2\":{\"value\":\"" + date + "\"},\"keyword3\":{\"value\": \"" + result + "\"}},\"emphasis_keyword\": \"keyword1.DATA\"}");
            
        }


        #region 生成二维码
        /// <summary>
        /// 生成二维码图片到本地
        /// </summary>
        /// <param name="unionid"></param>
        /// <returns></returns>
        public string GetWxCodeToLocal(string unionid)
        {
            var wxcode = new WxCodeModel()
            {
                scene = unionid,
                page = "pages/index/index",
                width = 430,
                auto_color = false,
                is_hyaline = false,
                // line_color = "{\"r\":\"0\",\"g\":\"0\",\"b\":\"0\"}"
            };
            var name = System.Web.HttpContext.Current.Server.MapPath("/Content/UploadImg/" + unionid + ".jpg");
            var accountToken = "12_3XH0TwhYm2_WTx07yD0kHO-jNH3dirK3Ktj97H-FHQ7RANMBtZKgjFq8-kbgCeiIwaUg-3728Q6wFcGeYxtBBkPsvTs--7E3wM8ipDGtNV2K9TufeQpOxy3TTpPawFCYPr5kxphi_hCYusv3QCPgAFABNA";
            var istrue = WebUtils.DoPostSaveImage("https://api.weixin.qq.com/wxa/getwxacodeunlimit?access_token=" + accountToken, JsonConvert.SerializeObject(wxcode), name);
            if (istrue)
            {
                return "http://localhost:62821/Content/UploadImg/" + unionid + ".jpg";
            }
            return "";

        }

        /// <summary>
        /// 生成二维码图片到数据库（保存二进制）
        /// </summary>
        /// <param name="unionid"></param>
        /// <returns></returns>
        public string GetWxCodeToData(string chanelid, string indirectchanel, string giftid, string unionid, int type = 0)
        {
            var scene = "";
            if (type == 0)
            {
                scene =  unionid;
            }
            if (type == 1)
            {
                unionid = chanelid + "_" + indirectchanel + "_" + giftid;
                scene =  chanelid + "_" + indirectchanel + "_" + giftid;
            }
            var imgurl = "http://localhost:62821/XiaoApi/WxCodeIndex?unionid=" + unionid;
            var wxcode = new WxCodeModel()
            {
                scene = scene,
                page = "pages/index/index",
                width = 430,
                auto_color = false,
                is_hyaline = false,
                // line_color = "{\"r\":\"0\",\"g\":\"0\",\"b\":\"0\"}"
            };
            var _WeiXinCodeDao = new WeiXinCodeDao();
            var code = _WeiXinCodeDao.GetByUnionid(unionid);//查找是否已经生成过
            if (code == null)
            {

                byte[] postdate; 
                var accountToken = "16_lt7_qia0hGG3_h3hxN7VXhp6dmZ7gBQ4tBzA--mCacT3xkhjmXIOcKvglP-Wsr1B9YLzykfLc7sUCkmZhNEl4JHoq1RyZ1KOBY_PNycbaihq682qqHujIwT9pfz-ui3oZ67hrokyHWN9IT7mQALjAEAGQB";
                var str = WebUtils.DoPostSaveImage("https://api.weixin.qq.com/wxa/getwxacodeunlimit?access_token=" + accountToken, JsonConvert.SerializeObject(wxcode), out postdate);
                var strtest = WebUtils.DoPost("https://api.weixin.qq.com/wxa/getwxacodeunlimit?access_token=" + accountToken, JsonConvert.SerializeObject(wxcode));
                if (str)
                { 
                    var _WeiXinCode = new WeiXinCode()
                    {
                        CreateTime = DateTime.Now,
                        ImageStream = postdate,
                        UnionId = unionid,
                        WxCodeUrl = "",
                    };
                    _WeiXinCodeDao.Add(_WeiXinCode);//保存二进制流到数据库
                    code = _WeiXinCode;

                }
            }
            if (code != null)
            {
                return imgurl;
            }
            return "";
        }

        /// <summary>
        /// 查询数据库获得图文二进制
        /// </summary>
        /// <param name="unionid"></param>
        /// <returns></returns>
        public byte[] WxCode(string unionid)
        {
            var _WeiXinCodeDao = new WeiXinCodeDao();
            var code = _WeiXinCodeDao.GetByUnionid(unionid);//查找是否已经生成过
            if (code != null)
            {
                return code.ImageStream;
            }
            return null;
        } 
        #endregion


    }

    /// <summary>
    /// 二维码提交数据模型
    /// </summary>
    public class WxCodeModel
    {
        public string  scene { get; set; }
        public string page { get; set; }
        public int width { get; set; }
        public bool auto_color { get; set; }
        public object line_color { get; set; }
        public bool is_hyaline { get; set; }
    } 
}
