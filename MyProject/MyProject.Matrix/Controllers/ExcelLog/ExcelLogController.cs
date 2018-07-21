using MyProject.Core.Entities;
using MyProject.Services.Npoi;
using MyProject.Task;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Matrix.Controllers.ExcelLog
{
    public class ExcelLogController : Controller
    {
        private readonly LogTask _log = new LogTask(); 

        public ActionResult Index()
        {
            return View();
        }

         
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase filebase)
        {
            HttpPostedFileBase file = Request.Files["files"];
            string FileName;
            string savePath;
            if (file == null || file.ContentLength <= 0)
            {
                ViewBag.error = "文件不能为空";
                return View();
            }
            else
            {
                string filename = Path.GetFileName(file.FileName);
                int filesize = file.ContentLength;//获取上传文件的大小单位为字节byte
                string fileEx = System.IO.Path.GetExtension(filename);//获取上传文件的扩展名
                string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//获取无扩展名的文件名
                int Maxsize = 4000 * 1024;//定义上传文件的最大空间大小为4M
                string FileType = ".xls,.xlsx";//定义上传文件的类型字符串

                FileName = NoFileName + fileEx;
                if (!FileType.Contains(fileEx))
                {
                    ViewBag.error = "文件类型不对，只能导入xls和xlsx格式的文件";
                    return View();
                }
                if (filesize >= Maxsize)
                {
                    ViewBag.error = "上传文件超过4M，不能上传";
                    return View();
                }
                string path = AppDomain.CurrentDomain.BaseDirectory + "/App_Data/";
                savePath = Path.Combine(path, FileName);
                file.SaveAs(savePath);
            }

            DataTable table =NpoiSdk.ExcelToDataTable(savePath);


            //引用事务机制，出错时，事物回滚
            using (TransactionScope transaction = new TransactionScope())
            {
                for (int i = 0; i < table.Rows.Count; i++)
                { 
                    string msg = table.Rows[i][0].ToString();
                    var ret = table.Rows[i][1];
                    var model = new Log()
                    {
                        Ret =Convert.ToInt32(ret),
                        Msg  = msg,
                        CreateTime  = DateTime.Now
                    };
                    _log.AddLog(model);
                    //此处写录入数据库代码；
                }
                transaction.Complete();
            }
            ViewBag.error = "导入成功";
            System.Threading.Thread.Sleep(2000);
            return View();
        }

        public FileResult GetFile()//获得模板下载地址
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "/App_Data/";
            string fileName = "配置信息.xls";
            return File(path + fileName, "text/plain", fileName);
        } 

    }
}
