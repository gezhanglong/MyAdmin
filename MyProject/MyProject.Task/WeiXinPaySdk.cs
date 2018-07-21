
using MyProject.Services.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


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
            string nonceStr = Utils.CreateNoncestr(16);
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
            var postData = Utils.ArrayToXml(dict);
            log = ("下订单postData" + postData + "," + 888);
            log += "###Signature：" + Signature(dict, PartnerKey);
            log += "###url：" + url + "####postData:" + postData;
            var result = WebUtils.DoPost(url, postData, Encoding.UTF8, "utf-8");

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
            dict.Add("nonceStr", Utils.CreateNoncestr(16));
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

        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="dict"></param>
        public static string Signature(Dictionary<string, string> dict, string partnerKey)
        {
            string str1 = dict.Where(d => d.Value != "").OrderBy(c => c.Key).Aggregate(String.Empty, (current, i) => current + String.Format("&{0}={1}", i.Key, i.Value));
            if (!String.IsNullOrEmpty(str1))
                str1 = str1.Substring(1);
            return CryptHelper.MD5Hash(str1 + "&key=" + partnerKey).ToUpper();
        }

        
    }
    [XmlRoot("xml")]
    public class UnifiedOrderReturn
    {
        public string return_code { get; set; }
        public string return_msg { get; set; }
        // 以下字段在return_code为 SUCCESS的时候有返回
        public string appid { get; set; }
        public string mch_id { get; set; }
        public string device_info { get; set; }
        public string nonce_str { get; set; }
        public string sign { get; set; }
        public string result_code { get; set; }
        public string err_code { get; set; }
        public string err_code_des { get; set; }
        // 以下字段在return_code 和 result_code都为 SUCCESS的时候有返回
        public string trade_type { get; set; }
        public string prepay_id { get; set; }
        public string mweb_url { get; set; }
    }
}
