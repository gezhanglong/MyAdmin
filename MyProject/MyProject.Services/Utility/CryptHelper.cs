using System;
using System.Security.Cryptography;
using System.Text;

namespace MyProject.Services.Utility
{
    /// <summary>
    /// 加密帮助类
    /// </summary>
    public static class CryptHelper
    {
        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string SHA1Hash(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            var sha1 = new SHA1CryptoServiceProvider();
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var outputBytes = sha1.ComputeHash(inputBytes);
            return BitConverter.ToString(outputBytes).Replace("-", "");
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string MD5Hash(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            var md5 = new MD5CryptoServiceProvider();
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var outputBytes = md5.ComputeHash(inputBytes);
            return BitConverter.ToString(outputBytes).Replace("-", "");
        }

        //md5加密算法，返回16位或32位，一般32位
        public static string MD5(string str, int code)
        {
            //返回16位
            string strEncrypt = string.Empty;
            if (code == 16)
            {
                var hashPasswordForStoringInConfigFile =
                    System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
                if (hashPasswordForStoringInConfigFile != null)
                    strEncrypt =
                        hashPasswordForStoringInConfigFile.Substring(8,
                                                                     16);
            }
            //返回32位
            if (code == 32)
            {
                strEncrypt = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
            }
            return strEncrypt;
        }
    }
}