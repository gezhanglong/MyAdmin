using MyProject.Core.Entities;
using MyProject.Services.Utility;
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

         
        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="filebase"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase filebase)
        {
            try
            {
                HttpPostedFileBase file = Request.Files["files"];
                var error = "";
                var savePath = NpoiSdk.GetSavePath(file, out error);
                if (string.IsNullOrWhiteSpace(savePath))
                {
                    ViewBag.error = error; 
                }
                DataTable table = NpoiSdk.ExcelToDataTable(savePath);


                //引用事务机制，出错时，事物回滚
                using (TransactionScope transaction = new TransactionScope())
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        string msg = table.Rows[i][0].ToString();
                        var ret = table.Rows[i][1];
                        var model = new Log()
                        {
                            Ret = Convert.ToInt32(ret),
                            Msg = msg,
                            CreateTime = DateTime.Now
                        };
                        _log.AddLog(model);
                        //此处写录入数据库代码；
                    }
                    transaction.Complete();
                } 
                ViewBag.error = "导入成功";
            }
            catch (Exception e)
            {
                ViewBag.error = "导入失败："+e.Message; 
            }
            System.Threading.Thread.Sleep(2000);
            return View();
        }

        public FileResult GetFile()//获得模板下载地址
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "/App_Data/";
            string fileName = "配置信息.xls";
            return File(path + fileName, "text/plain", fileName);
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        public ActionResult OutFile()
        {
            var logList = _log.GetList();
            string path = @"E:\";
            var _book =new BuildWorkBook<Log>(new Log());
            var error = "导出成功，导出路径：" + path;
            _book.OutFile(logList, path, ref error);
            return Content(error); 
        }


        public ActionResult SaveImage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SaveImage(HttpPostedFileBase filebase)
        {
            var files = Request.Files;
            var msg = "";
            ImgHelper.UploadImag(files, ref msg);
            ViewBag.error = msg;
            return View();
        }

    }
}
