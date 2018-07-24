using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyProject.Services.Npoi
{
    public class NpoiSdk
    {

        /// <summary>
        /// 文件上传后服务器地址
        /// </summary>
        /// <param name="file"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static string GetSavePath(HttpPostedFileBase file,out string error)
        {
            string FileName;
            string savePath=string.Empty;
            error = "";
            if (file == null || file.ContentLength <= 0)
            {
                error = "文件不能为空";
                return savePath;
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
                    error = "文件类型不对，只能导入xls和xlsx格式的文件";
                    return savePath;
                }
                if (filesize >= Maxsize)
                {
                    error = "上传文件超过4M，不能上传";
                    return savePath;
                }
                string path = AppDomain.CurrentDomain.BaseDirectory + "/App_Data/";
                savePath = Path.Combine(path, FileName);
                file.SaveAs(savePath);
                return savePath;
            }
        }
        /// <summary>读取excel
        ///导入 根据索引读取Sheet表数据，默认读取第一个sheet
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <param name="sheetIndex">sheet表的索引，从0开始</param>
        /// <returns>数据集</returns>
        public static DataTable ExcelToDataTable(string strFileName, int sheetIndex = 0)
        {
            DataTable dt = new DataTable();
            HSSFWorkbook hssfworkbook = null;
            XSSFWorkbook xssfworkbook = null;
            string fileExt = Path.GetExtension(strFileName);//获取文件的后缀名
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                if (fileExt == ".xls")
                    hssfworkbook = new HSSFWorkbook(file);
                else if (fileExt == ".xlsx")
                    xssfworkbook = new XSSFWorkbook(file);//初始化太慢了，不知道这是什么bug
            }
            if (hssfworkbook != null)
            {
                HSSFSheet sheet = (HSSFSheet)hssfworkbook.GetSheetAt(sheetIndex);
                if (sheet != null)
                {
                    System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
                    HSSFRow headerRow = (HSSFRow)sheet.GetRow(0);
                    int cellCount = headerRow.LastCellNum;
                    for (int j = 0; j < cellCount; j++)
                    {
                        HSSFCell cell = (HSSFCell)headerRow.GetCell(j);
                        dt.Columns.Add(cell.ToString());
                    }
                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                    {
                        HSSFRow row = (HSSFRow)sheet.GetRow(i);
                        DataRow dataRow = dt.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            if (row.GetCell(j) != null)
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        dt.Rows.Add(dataRow);
                    }
                }
            }
            else if (xssfworkbook != null)
            {
                XSSFSheet xSheet = (XSSFSheet)xssfworkbook.GetSheetAt(sheetIndex);
                if (xSheet != null)
                {
                    System.Collections.IEnumerator rows = xSheet.GetRowEnumerator();
                    XSSFRow headerRow = (XSSFRow)xSheet.GetRow(0);
                    int cellCount = headerRow.LastCellNum;
                    for (int j = 0; j < cellCount; j++)
                    {
                        XSSFCell cell = (XSSFCell)headerRow.GetCell(j);
                        dt.Columns.Add(cell.ToString());
                    }
                    for (int i = (xSheet.FirstRowNum + 1); i <= xSheet.LastRowNum; i++)
                    {
                        XSSFRow row = (XSSFRow)xSheet.GetRow(i);
                        DataRow dataRow = dt.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            if (row.GetCell(j) != null)
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        dt.Rows.Add(dataRow);
                    }
                }
            }
            return dt;
        }

       
    }

    public class BuildWorkBook<T>
    {
        public T model { get; set; }
        public BuildWorkBook(T model)
        {
            this.model =model;
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="models"></param>
        /// <param name="FilePath"></param>
        public bool OutFile( List<T> models, string FilePath,ref string error)
        {
            try
            {
                HSSFWorkbook workbook = new HSSFWorkbook();
                var sheet = workbook.CreateSheet("sheet1");
                var rowHead = sheet.CreateRow(0);
                var i = 0;
                foreach (System.Reflection.PropertyInfo p in this.model.GetType().GetProperties())
                {
                    rowHead.CreateCell(i).SetCellValue(p.Name);
                    i++;
                }
                for (int rowIndex = 0; rowIndex < models.Count(); rowIndex++)
                {
                    var row = sheet.CreateRow(rowIndex + 1);
                    var j = 0;
                    foreach (System.Reflection.PropertyInfo p in models[rowIndex].GetType().GetProperties())
                    {
                        row.CreateCell(j).SetCellValue((p.GetValue(models[rowIndex])).ToString());
                        j++;
                    }
                }
                using (FileStream fs = new FileStream(FilePath + DateTime.Now.ToString("yyyy-MM-dd") + ".xls", FileMode.Create))
                {
                    workbook.Write(fs);
                }
                return true;
            }
            catch (Exception e)
            {
                error = e.Message;
                return false;
            } 
        }
    }
}
