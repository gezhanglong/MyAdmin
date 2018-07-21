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
        /// <summary>读取excel
        /// 根据索引读取Sheet表数据，默认读取第一个sheet
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
}
