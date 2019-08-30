using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JobWinFormApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
 


        private void Form1_Load(object sender, EventArgs e)
        {
            InternetSetOption("125.118.10.110:61234");
            WebBrowser browser = new WebBrowser();
            browser.ScriptErrorsSuppressed = true;
            browser.Dock = DockStyle.Fill;
            this.Controls.Add(browser);
            browser.Navigate("https://jingyan.baidu.com/article/67508eb4951138dcca1ce4dc.html");//redis

            label1.Text = "你好！开始"+DateTime.Now;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }


        public struct Struct_INTERNET_PROXY_INFO
        {
            public int dwAccessType;
            public IntPtr proxy;
            public IntPtr proxyBypass;
        };



        //You can change the proxy with InternetSetOption method from the wininet.dll, here is a example to set the proxy

        //这个就是设置一个Internet 选项，其实就是可以设置一个代理
        [DllImport("wininet.dll", SetLastError = true)]
        private static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int lpdwBufferLength);



        //设置代理的方法

        //strProxy为代理IP:端口
        private void InternetSetOption(string strProxy)
        {

            //设置代理选项

            const int INTERNET_OPTION_PROXY = 38;

            //设置代理类型
            const int INTERNET_OPEN_TYPE_PROXY = 3;

            //设置代理类型，直接访问，不需要通过代理服务器了

            const int INTERNET_OPEN_TYPE_DIRECT = 1;


            Struct_INTERNET_PROXY_INFO struct_IPI;
            // Filling in structure 
            struct_IPI.dwAccessType = INTERNET_OPEN_TYPE_PROXY;

            //把代理地址设置到非托管内存地址中 
            struct_IPI.proxy = Marshal.StringToHGlobalAnsi(strProxy);

            //代理通过本地连接到代理服务器上
            struct_IPI.proxyBypass = Marshal.StringToHGlobalAnsi("local");

            // Allocating memory 

            //关联到内存
            IntPtr intptrStruct = Marshal.AllocCoTaskMem(Marshal.SizeOf(struct_IPI));
            if (string.IsNullOrEmpty(strProxy) || strProxy.Trim().Length == 0)
            {
                strProxy = string.Empty;
                struct_IPI.dwAccessType = INTERNET_OPEN_TYPE_DIRECT;
            }

            // Converting structure to IntPtr 

            //把结构体转换到句柄
            Marshal.StructureToPtr(struct_IPI, intptrStruct, true);
            bool iReturn = InternetSetOption(IntPtr.Zero, INTERNET_OPTION_PROXY, intptrStruct, Marshal.SizeOf(struct_IPI));
        }

    }
}
