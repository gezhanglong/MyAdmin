using System.Text;

namespace MyProject.Services.Utility
{
    public class EncodeUtility
    {
        /// <summary>
        ///   国标码和区位码转换常量
        /// </summary>
        public static int GB_SP_DIFF = 160;

        /// <summary>
        /// 存放国标一级汉字不同读音的起始区位码 
        /// </summary>
        public static int[] SecPosvalueList = { 1601, 1637, 1833, 2078, 2274, 2302, 2433, 2594, 2787, 3106, 3212, 3472,
                                            3635, 3722, 3730, 3858, 4027, 4086, 4390, 4558, 4684, 4925, 5249, 5600 };
        /// <summary>
        /// 存放国标一级汉字不同读音的起始区位码对应读音
        /// </summary>
        public static char[] FirstLetter = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k', 'l', 'm',
                                          'n', 'o', 'p', 'q', 'r', 's', 't', 'w', 'x', 'y', 'z' };

        /// <summary>
        /// 获取一个字符串的拼音码
        /// </summary>
        /// <param name="orginString">原始字符串</param>
        /// <returns></returns>
        public static string GetFirstLetters(string orginString)
        {
            var str = orginString.ToLower();
            var buffer = new StringBuilder();
            foreach (var ch in str)
            {
                var temp = new char[] { ch };
                var uniCode = Encoding.Default.GetBytes(temp);
                if (uniCode[0] < 128 && uniCode[0] > 0)
                {
                    //非汉字   
                    buffer.Append(temp);
                }
                else
                {
                    buffer.Append(Convert(uniCode));
                }
            }
            return buffer.ToString();
        }

        /// <summary>
        /// 获取一个汉字的拼音首字母。   
        /// GB码两个字节分别减去160，转换成10进制码组合就可以得到区位码   
        /// 例如汉字“你”的GB码是0xC4/0xE3，分别减去0xA0（160）就是0x24/0x43   
        /// 0x24转成10进制就是36，0x43是67，那么它的区位码就是3667，在对照表中读音为‘n’  
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static char Convert(byte[] bytes)
        {
            var result = '-';
            var secPosvalue = 0;
            int i;
            for (i = 0; i < bytes.Length; i++)
            {
                bytes[i] -= (byte)GB_SP_DIFF;
            }
            secPosvalue = bytes[0] * 100 + bytes[1];
            for (i = 0; i < 23; i++)
            {
                if (secPosvalue >= SecPosvalueList[i] && secPosvalue < SecPosvalueList[i + 1])
                {
                    result = FirstLetter[i];
                    break;
                }
            }
            return result;
        }
    }
}