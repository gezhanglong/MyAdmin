using MyProject.Core.Entities;
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
        private readonly ExcelSdk _excelSdk = new ExcelSdk();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string filePath)
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

                var personList = new List<Log>();
                var errors = "";
                //校验数据is
                bool checkResult = _excelSdk.CheckImportData(savePath, personList, ref errors);
                //校验通过直接保存
                if (checkResult)
                {
                    _excelSdk.SaveImportData(personList);
                    _log.AddLog(new Log() { Msg = "导入成功", Ret = 0, CreateTime = DateTime.Now });
                    ViewBag.error = "导入成功";
                    return View();
                }
                else
                {
                    ViewBag.error = "导入失败";
                    return View();
                }

            }
        }

        //[HttpPost]
        //public ActionResult Index(HttpPostedFileBase filebase)
        //{
        //    HttpPostedFileBase file = Request.Files["files"];
        //    string FileName;
        //    string savePath;
        //    if (file == null || file.ContentLength <= 0)
        //    {
        //        ViewBag.error = "文件不能为空";
        //        return View();
        //    }
        //    else
        //    {
        //        string filename = Path.GetFileName(file.FileName);
        //        int filesize = file.ContentLength;//获取上传文件的大小单位为字节byte
        //        string fileEx = System.IO.Path.GetExtension(filename);//获取上传文件的扩展名
        //        string NoFileName = System.IO.Path.GetFileNameWithoutExtension(filename);//获取无扩展名的文件名
        //        int Maxsize = 4000 * 1024;//定义上传文件的最大空间大小为4M
        //        string FileType = ".xls,.xlsx";//定义上传文件的类型字符串

        //        FileName = NoFileName + fileEx;
        //        if (!FileType.Contains(fileEx))
        //        {
        //            ViewBag.error = "文件类型不对，只能导入xls和xlsx格式的文件";
        //            return View();
        //        }
        //        if (filesize >= Maxsize)
        //        {
        //            ViewBag.error = "上传文件超过4M，不能上传";
        //            return View();
        //        }
        //        string path = AppDomain.CurrentDomain.BaseDirectory + "/App_Data/";
        //        savePath = Path.Combine(path, FileName);
        //        file.SaveAs(savePath);
        //    }
        //    string strConn;
        //    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + savePath + ";" + "Extended Properties=Excel 12.0";
        //    //strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + savePath + ";" + "Extended Properties=Excel 8.0";
           
        //    OleDbConnection conn = new OleDbConnection(strConn);
        //    conn.Open();
        //    OleDbDataAdapter myCommand = new OleDbDataAdapter("select * from [Sheet1$]", strConn);
        //    DataSet myDataSet = new DataSet();
           
        //    conn.Close();
        //    try
        //    {
        //        myCommand.Fill(myDataSet, "ExcelInfo");
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.error = ex.Message;
        //        return View();
        //    }
        //    DataTable table = myDataSet.Tables["ExcelInfo"].DefaultView.ToTable();

        //    //引用事务机制，出错时，事物回滚
        //    using (TransactionScope transaction = new TransactionScope())
        //    {
        //        for (int i = 0; i < table.Rows.Count; i++)
        //        { 
        //            string msg = table.Rows[i][1].ToString();
        //            int ret = int.Parse(table.Rows[i][2].ToString());
        //            var model = new Log()
        //            { 
        //                Msg = msg,
        //                Ret = ret,
        //                CreateTime=DateTime.Now
        //            };
        //            //此处写录入数据库代码；
        //            _log.AddLog(model);
        //        }
        //        transaction.Complete();
        //    }
        //    ViewBag.error = "导入成功";
        //    System.Threading.Thread.Sleep(2000);
        //    return View(); // RedirectToAction("Excel1");
        //}

        public FileResult GetFile()//获得模板下载地址
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "/App_Data/";
            string fileName = "配置信息.xls";
            return File(path + fileName, "text/plain", fileName);
        } 

    }
}
