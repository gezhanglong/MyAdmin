using MyProject.Core.Dtos;
using MyProject.Core.Entities;
using MyProject.Data.Daos;
using MyProject.Services.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MyProject.Task
{
    public class WeiXinPayNotifySdk
    {
        private readonly LogDao _log = new LogDao();
        private SortedDictionary<string, object> values = new SortedDictionary<string, object>();

        public RequestResultDto PayNotify(string msgBody)
        {
            RequestResultDto requestResult = new RequestResultDto { Ret = 0, Msg = "Ok" };
            Dictionary<string, string> dict = new Dictionary<string, string>
                           {
                               {"return_code","SUCCESS"},
                               {"return_msg","Ok"}
                           };
            try
            {
                PayNotifyInfo pnInfo = XmlHelper.XmlToEntity<PayNotifyInfo>(msgBody);
                _log.Add(new Log() { Msg = "return_code:" + pnInfo.return_code + ",return_msg:" + pnInfo.return_msg + ",pnInfo.out_trade_no:" + pnInfo.out_trade_no + ",pnInfo.total_fee:" + pnInfo.total_fee, Ret = 168, CreateTime = DateTime.Now }); 
                if (pnInfo != null && pnInfo.return_code == "SUCCESS" && pnInfo.result_code == "SUCCESS")
                {
                    #region 做自己的业务
                    if (pnInfo.appid == "")
                    {
                        string partnerKey = "";
                        if (!GetSignAndCheckSign(msgBody, partnerKey))
                        {
                            dict["return_code"] = "FAIL";
                            dict["return_msg"] = "签名验证错误";
                            requestResult.Msg = Utils.ArrayToXml(dict);
                            return requestResult;
                        }
 
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                _log.Add(new Log() { Msg = "error:" + ex.ToString(), Ret = 168, CreateTime = DateTime.Now });  
                dict["return_code"] = "FAIL";
                dict["return_msg"] = "系统错误";
            }
            requestResult.Msg = Utils.ArrayToXml(dict);
            return requestResult;
        }
        #region 签名验证辅助方法
        public bool GetSignAndCheckSign(string xml, string PartnerKey)
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            XmlNode xmlNode = xmlDoc.FirstChild; //获取到根节点<xml>
            XmlNodeList nodes = xmlNode.ChildNodes;
            foreach (XmlNode xn in nodes)
            {
                XmlElement xe = (XmlElement)xn;
                values[xe.Name] = xe.InnerText; //获取xml的键值对到WxPayData内部的数据中
            }

            if (CheckSign(PartnerKey)) //验证签名,不通过会抛异常
            {
                return true;
            }
            return false;
        }

        public bool CheckSign(string PartnerKey)
        {
            //如果没有设置签名，则跳过检测

            if (!IsSet("sign"))
            {
                _log.Add(new Log() { Msg = "签名存在但不合法" , Ret = 168, CreateTime = DateTime.Now });
                throw new Exception("签名存在但不合法!");
            }
            //如果设置了签名但是签名为空，则抛异常
            else if (GetValue("sign") == null || GetValue("sign").ToString() == "")
            {
                _log.Add(new Log() { Msg = "签名存在但不合法", Ret = 168, CreateTime = DateTime.Now });
                throw new Exception("签名存在但不合法");
            }

            //获取接收到的签名
            string return_sign = GetValue("sign").ToString();

            //在本地计算新的签名

            string cal_sign = MakeSign(PartnerKey);

            if (cal_sign == return_sign)
            {
                return true;
            }
            _log.Add(new Log() { Msg = "签名存在但不合法", Ret = 168, CreateTime = DateTime.Now }); 
            throw new Exception("WxPayData签名验证错误!");
        }
        public object GetValue(string key)
        {
            object o = null;
            values.TryGetValue(key, out o);
            return o;
        }
        public bool IsSet(string key)
        {
            object o = null;
            values.TryGetValue(key, out o);
            if (null != o)
                return true;
            else
                return false;
        }
        public string MakeSign(string PartnerKey)
        {
            string str = ToUrl();
            str += "&key=" + PartnerKey;
            var md5 = MD5.Create();
            var bs = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            var sb = new StringBuilder();
            foreach (byte b in bs)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString().ToUpper();
        }
        public string ToUrl()
        {
            string buff = "";
            foreach (KeyValuePair<string, object> pair in values)
            {
                if (pair.Value == null)
                {
                    _log.Add(new Log() { Msg = "内部含有值为null的字段", Ret = 168, CreateTime = DateTime.Now });
                    throw new Exception("内部含有值为null的字段");
                }

                if (pair.Key != "sign" && pair.Value.ToString() != "")
                {
                    buff += pair.Key + "=" + pair.Value + "&";
                }
            }
            buff = buff.Trim('&');
            return buff;
        }
        #endregion
    }

    public class PayNotifyInfo
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
        public string openid { get; set; }
        public string trade_type { get; set; }
        public string bank_type { get; set; }

        public string total_fee { get; set; }
        public string cash_fee { get; set; }
        public string transaction_id { get; set; }

        public string out_trade_no { get; set; }
        public string time_end { get; set; } 
    }
}
