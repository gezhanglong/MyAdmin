using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Task
{
    public class WeiXinPaySdk
    {
        /// <summary>
        /// 支付统一接口（这个是小程序的统一下单接口，其他支付就参数不一样，用到的时候看文档加参数使用）
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="totalFee"></param>
        /// <param name="ip"></param>
        /// <param name="orderNo"></param>
        /// <param name="tradeType"></param>
        /// <param name="body"></param>
        /// <param name="appId"></param>
        /// <param name="MchId"></param>
        /// <param name="PartnerKey"></param>
        /// <param name="notifyUrl"></param>
        /// <param name="uoReturn"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public bool UnifiedOrder(string openId, int totalFee, string ip, string orderNo, string tradeType, string body, string appId, string MchId, string PartnerKey, string notifyUrl, out UnifiedOrderReturn uoReturn, out string log)
        {
            //统一支付接口
            var url = "https://api.mch.weixin.qq.com/pay/unifiedorder";
            string nonceStr = LGPartnerAPI.StringUtil.CreateNoncestr(16);
            var dict = new Dictionary<string, string>
                           {
                               {"appid", appId},
                               {"mch_id", MchId},
                               {"nonce_str", nonceStr},
                               {"body", body}, 
                               {"out_trade_no", orderNo},
                               {"total_fee", totalFee.ToString()},
                               {"spbill_create_ip", ip},
                               {"notify_url", notifyUrl}, 
                               {"trade_type", tradeType}, 
                           };
            if (!String.IsNullOrEmpty(openId)) dict.Add("openid", openId);
            dict.Add("sign", Signature(dict, PartnerKey));
            var postData = LGPartnerAPI.StringUtil.ArrayToXml(dict);
            log = ("下订单postData" + postData + "," + 888);
            log += "###Signature：" + Signature(dict, PartnerKey);
            log += "###url：" + url + "####postData:" + postData;
            var result = LGPartnerAPI.StringUtil.DoPost(url, postData, Encoding.UTF8, "utf-8");

            log += ("下订单result" + result + "," + 888);
            uoReturn = XmlHelper.XmlToEntity<UnifiedOrderReturn>(result);
            if (uoReturn != null && uoReturn.return_code == "SUCCESS" && uoReturn.result_code == "SUCCESS" && !String.IsNullOrEmpty(uoReturn.prepay_id))
                return true;
            return false;
        }

        public string GetBackage(string prepayId, string appId, string PartnerKey)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("appId", appId);
            dict.Add("timeStamp", GetTimeZ(DateTime.Now).ToString());
            dict.Add("nonceStr", LGPartnerAPI.StringUtil.CreateNoncestr(16));
            dict.Add("package", "prepay_id=" + prepayId);
            dict.Add("signType", "MD5");
            var str1 = "{";
            str1 += String.Format("\"appId\":\"{0}\"", dict["appId"]);
            str1 += String.Format(",\"timeStamp\":\"{0}\"", dict["timeStamp"]);
            str1 += String.Format(",\"nonceStr\":\"{0}\"", dict["nonceStr"]);
            str1 += String.Format(",\"package\":\"{0}\"", dict["package"]);
            str1 += String.Format(",\"signType\":\"MD5\"");
            str1 += String.Format(",\"paySign\":\"{0}\"", Signature(dict, PartnerKey));
            str1 += "}";
            return str1;
        }
        public int GetTimeZ(DateTime time)
        {
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        
    }
}
