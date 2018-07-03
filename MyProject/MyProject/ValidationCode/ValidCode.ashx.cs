using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace MyProject.ValidationCode
{
    /// <summary>
    /// C09ValidCode 生成验证码图片
    /// </summary>
    public class ValidCode : IHttpHandler,System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "image/jpeg";
            //初始化 字符
            charArr = new char[10] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            //生成随机验证码字符串
            string code = MakeRanStr(4);

            //将验证码 存入 服务器端 Session
            context.Session["code"] = code;

            //生成验证码图片
            using (Image img = new Bitmap(100, 30))
            {
                using (Graphics g = Graphics.FromImage(img))
                {
                    g.Clear(Color.White);
                    DrawGanRaoDian(50, g, img);
                    g.DrawRectangle(Pens.Black, new Rectangle(0, 0, img.Width-1, img.Height-1));
                    g.DrawString(code, new Font("微软雅黑", 16), Brushes.Red, 0, 2);
                    //DrawGanRaoDian(50, g, img);
                }
                //将生成 图片 保存到 响应报文体流中，以jpg图片格式保存
                img.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        #region 1.0 画干扰点 -DrawGanRaoDian(int count, Graphics g, Image img)
        /// <summary>
        /// 画干扰点
        /// </summary>
        /// <param name="count"></param>
        /// <param name="g"></param>
        /// <param name="img"></param>
        void DrawGanRaoDian(int count, Graphics g, Image img)
        {
            for (int i = 0; i < count; i++)
            {
                Point p1 = new Point(ran.Next(img.Width - 5), ran.Next(img.Height - 5));
                Point p2 = new Point(p1.X - ran.Next(10), p1.Y - ran.Next(10));
                g.DrawLine(Pens.Black, p1, p2);
            }
        } 
        #endregion

        /// <summary>
        /// 准备的字符
        /// </summary>
        char[] charArr;
        /// <summary>
        /// 随机对象
        /// </summary>
        Random ran = new Random();

        #region 1.随机生成字符串 -string MakeRanStr()
        /// <summary>
        /// 1.随机生成字符串
        /// </summary>
        /// <returns></returns>
        string MakeRanStr(int charLength)
        {
            System.Text.StringBuilder sbCode = new System.Text.StringBuilder(5);
            int index = -1;
            for (int i = 0; i < charLength; i++)
            {
                index = ran.Next(charArr.Length);//0<= x < 10
                sbCode.Append(charArr[index]);
            }
            return sbCode.ToString();
        } 
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}