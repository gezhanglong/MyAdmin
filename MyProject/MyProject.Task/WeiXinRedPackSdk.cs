using MyProject.Core.Dtos;
using MyProject.Core.Entities;
using MyProject.Data.Daos;
using MyProject.Services.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyProject.Task
{
    public class WeiXinRedPackSdk
    {
        private const string MchId = "1416319802"; 
        private const string PartnerKey = "edg8p9BE1uolapAlpc4o79dlvN3Feny8"; 
        private const string cert = @"C:\\m.mj.lkgame.com\\kwx\\apiclient_cert.p12";
        private readonly LogDao _log = new LogDao();
        /// <summary>
        /// 发红包接口
        /// </summary>
        /// <param name="reOpenId">接收红包的用户</param>
        /// <param name="totalamount">付款金额（分）</param>
        /// <param name="wishing"></param>
        /// <param name="actName"></param>
        /// <param name="remark"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public SendRedPackReturn SendRedPack(string reOpenId, string totalamount, string wishing, string actName, string remark, string sendName, string mchId, string partnerKey, string wxappId, string cert, out RequestResultDto result)
        {
            SendRedPackReturn srpReturn = null;
            result = new RequestResultDto { Ret = 0, Msg = "ok" };
            try
            {
                string url = "https://api.mch.weixin.qq.com/mmpaymkttransfers/sendredpack";

                string orderNo = mchId + DateTime.Now.ToString("yyyyMMdd") + Utils.CreateNoncestr(10); //商户订单号

                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                X509Certificate2 cer = new X509Certificate2(cert, mchId, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
                HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(url);
                webrequest.ClientCertificates.Add(cer);
                webrequest.Method = "post";

                var dict = new Dictionary<string, string>
                           {
                               {"nonce_str",Utils.CreateNoncestr(16)},
                               {"mch_billno", orderNo},
                               {"mch_id", mchId},
                               {"wxappid", wxappId},
                               {"nick_name", sendName},
                               {"send_name", sendName},
                               {"re_openid", reOpenId},
                               {"total_amount", totalamount},
                               {"min_value", totalamount},
                               {"max_value", totalamount},
                               {"total_num", "1"}, 
                               {"wishing", wishing}, 
                               {"client_ip", Utils.GetIp()}, 
                               {"act_name", actName}, 
                               {"remark", remark}, 
                           };
                dict.Add("sign", Signature(dict, partnerKey));
                var postDataStr = Utils.ArrayToXml(dict);
                _log.Add(new Log(){Msg=postDataStr,Ret=168,CreateTime=DateTime.Now});
                byte[] postData = Encoding.UTF8.GetBytes(postDataStr);
                Stream reqStream = webrequest.GetRequestStream();
                reqStream.Write(postData, 0, postData.Length);
                reqStream.Close();

                HttpWebResponse webreponse = (HttpWebResponse)webrequest.GetResponse();
                Stream stream = webreponse.GetResponseStream();
                string resp = string.Empty;
                using (StreamReader reader = new StreamReader(stream))
                {
                    resp = reader.ReadToEnd(); 
                    _log.Add(new Log() { Msg = resp, Ret = 168, CreateTime = DateTime.Now });
                }
                srpReturn = XmlHelper.XmlToEntity<SendRedPackReturn>(resp);
            }
            catch (Exception exp)
            {
                result = new RequestResultDto { Ret = 10009, Msg = "出错，" + exp.Message };
                _log.Add(new Log() { Msg = exp.ToString(), Ret = 168, CreateTime = DateTime.Now }); 
            }
            return srpReturn;
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            if (errors == SslPolicyErrors.None)
                return true;
            return false;
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

        /// <summary>
        /// 企业付款
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="totalamount">钱（单位：分）</param>
        /// <param name="desc">描述</param>
        /// <param name="mchId">商户ID</param>
        /// <param name="partnerKey">商户key</param>
        /// <param name="wxappId">公众号appid</param>
        /// <param name="cert">证书地址</param>
        /// <param name="result"></param>
        /// <returns></returns>
        public EnterprisePayReturn EnterprisePay(string openId, string totalamount, string desc, string mchId, string partnerKey, string wxappId, string cert, out RequestResultDto result)
        {
            EnterprisePayReturn srpReturn = null;
            result = new RequestResultDto { Ret = 0, Msg = "ok" };
            try
            {
                string url = "https://api.mch.weixin.qq.com/mmpaymkttransfers/promotion/transfers";

                string orderNo = mchId + DateTime.Now.ToString("yyyyMMdd") +Utils.CreateNoncestr(10); //商户订单号

                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                X509Certificate2 cer = new X509Certificate2(cert, mchId, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
                HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(url);
                webrequest.ClientCertificates.Add(cer);
                webrequest.Method = "post";

                var dict = new Dictionary<string, string>
                           {
                               {"nonce_str",Utils.CreateNoncestr(16)}, 
                               {"mchid", mchId},
                               {"mch_appid", wxappId},
                               {"partner_trade_no", orderNo},
                               {"openid", openId},
                               {"check_name", "NO_CHECK"},
                               {"amount", totalamount},
                               {"desc", desc}, 
                               {"spbill_create_ip", Utils.GetIp()},   
                           };
                dict.Add("sign", Signature(dict, partnerKey));
                var postDataStr = Utils.ArrayToXml(dict); 
                _log.Add(new Log() { Msg = postDataStr, Ret = 168, CreateTime = DateTime.Now }); 
                byte[] postData = Encoding.UTF8.GetBytes(postDataStr);
                Stream reqStream = webrequest.GetRequestStream();
                reqStream.Write(postData, 0, postData.Length);
                reqStream.Close();

                HttpWebResponse webreponse = (HttpWebResponse)webrequest.GetResponse();
                Stream stream = webreponse.GetResponseStream();
                string resp = string.Empty;
                using (StreamReader reader = new StreamReader(stream))
                {
                    resp = reader.ReadToEnd();
                    _log.Add(new Log() { Msg = resp, Ret = 168, CreateTime = DateTime.Now });  
                }
                srpReturn = XmlHelper.XmlToEntity<EnterprisePayReturn>(resp);
            }
            catch (Exception exp)
            {
                result = new RequestResultDto { Ret = 10009, Msg = "出错，" + exp.Message };
                _log.Add(new Log() { Msg = exp.ToString(), Ret = 168, CreateTime = DateTime.Now });  
            }
            return srpReturn;
        }

        //测试服务器上是否已经安装DigiCert根CA证书
        //public testSendRedPackReturn TestCert(string mchId, string partnerKey)
        //{
        //    testSendRedPackReturn srpReturn = null; 
        //    try
        //    { 
        //    string url = "https://apitest.mch.weixin.qq.com/sandboxnew/pay/getsignkey";


        //    HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(url); 
        //    webrequest.Method = "post";

        //    var dict = new Dictionary<string, string>
        //                   {
        //                       {"nonce_str",Utils.CreateNoncestr(16)} ,
        //                       {"mch_id", mchId} 
        //                   };
        //    dict.Add("sign", Signature(dict, partnerKey));
        //    var postDataStr = StringUtil.ArrayToXml(dict);
        //    WeixinSdkTask.Log("3221" + postDataStr, 322);
        //    byte[] postData = Encoding.UTF8.GetBytes(postDataStr);
        //    Stream reqStream = webrequest.GetRequestStream();
        //    reqStream.Write(postData, 0, postData.Length);
        //    reqStream.Close();

        //    HttpWebResponse webreponse = (HttpWebResponse)webrequest.GetResponse();
        //    Stream stream = webreponse.GetResponseStream();
        //    string resp = string.Empty;
        //    using (StreamReader reader = new StreamReader(stream))
        //    {
        //        resp = reader.ReadToEnd();
        //        WeixinSdkTask.Log("3222"+resp, 322);
        //    }
        //    srpReturn = XmlHelper.XmlToEntity<testSendRedPackReturn>(resp);
        //    WeixinSdkTask.Log("3223" + srpReturn.ToString(), 322);
        //    }
        //    catch (Exception exp)
        //    {
        //        WeixinSdkTask.Log("3224" + exp.ToString(), 322);
        //    }
        //    return srpReturn;
        //}
    }

    //测试服务器上是否已经安装DigiCert根CA证书
    //[XmlRoot("xml")]
    //public class testSendRedPackReturn
    //{
    //    public string return_code { get; set; }
    //    public string return_msg { get; set; }
    //    // 以下字段在return_code为 SUCCESS的时候有返回
    //    public string sign { get; set; }
    //    public string result_code { get; set; }
    //    public string err_code { get; set; }
    //    public string err_code_des { get; set; }
    //    // 以下字段在return_code 和 result_code都为 SUCCESS的时候有返回
    //    public string mch_billno { get; set; }
    //    public string mch_id { get; set; }
    //    public string wxappid { get; set; }
    //    public string re_openid { get; set; }
    //    public string total_amount { get; set; }
    //    public string send_listid { get; set; }
    //    public string sandbox_signkey { get; set; }
    //}

    [XmlRoot("xml")]
    public class SendRedPackReturn
    {
        public string return_code { get; set; }
        public string return_msg { get; set; }
        // 以下字段在return_code为 SUCCESS的时候有返回
        public string sign { get; set; }
        public string result_code { get; set; }
        public string err_code { get; set; }
        public string err_code_des { get; set; }
        // 以下字段在return_code 和 result_code都为 SUCCESS的时候有返回
        public string mch_billno { get; set; }
        public string mch_id { get; set; }
        public string wxappid { get; set; }
        public string re_openid { get; set; }
        public string total_amount { get; set; }
        public string send_listid { get; set; }
    }

    [XmlRoot("xml")]
    public class EnterprisePayReturn
    {
        public string return_code { get; set; }
        public string return_msg { get; set; }
        // 以下字段在return_code为 SUCCESS的时候有返回
        public string mch_appid { get; set; }
        public string mchid { get; set; }
        public string result_code { get; set; }
        public string err_code { get; set; }
        public string err_code_des { get; set; }
        // 以下字段在return_code 和 result_code都为 SUCCESS的时候有返回
        public string partner_trade_no { get; set; }
        public string payment_no { get; set; }
        public string payment_time { get; set; }
    }
}
