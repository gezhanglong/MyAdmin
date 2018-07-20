using LinqToExcel;
using MyProject.Core.Entities;
using MyProject.Data.Daos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Task
{
    public class ExcelSdk
    {
         /// <summary>
        /// 校验Excel数据
        /// </summary>
        public bool CheckImportData(string fileName, List<Log> personList, ref string errors)
        {
          
            var targetFile = new FileInfo(fileName);

            if (!targetFile.Exists)
            {

                errors="导入的数据文件不存在";
                return false;
            }

            var excelFile = new ExcelQueryFactory(fileName);

            //对应列头
            excelFile.AddMapping<Log>(x => x.Id, "Id");
            excelFile.AddMapping<Log>(x => x.Msg, "Msg");
            excelFile.AddMapping<Log>(x => x.Ret, "Ret");
            excelFile.AddMapping<Log>(x => x.CreateTime, "CreateTime"); 
            //SheetName
            var excelContent = excelFile.Worksheet<Log>(0);

            int rowIndex = 1;

            //检查数据正确性
            foreach (var row in excelContent)
            {
                var errorMessage = new StringBuilder();
                var person = new Log();

                person.Msg = row.Msg;
                person.Ret = row.Ret;
                person.CreateTime = row.CreateTime; 

                //if (string.IsNullOrWhiteSpace(row.Name))
                //{
                //    errorMessage.Append("Name - 不能为空. ");
                //}

                //if (string.IsNullOrWhiteSpace(row.IDCard))
                //{
                //    errorMessage.Append("IDCard - 不能为空. ");
                //}

                
                if (errorMessage.Length > 0)
                {
                    errors=string.Format(
                        "第 {0} 列发现错误：{1}{2}",
                        rowIndex,
                        errorMessage,
                        "<br/>");
                }
                personList.Add(person);
                rowIndex += 1;
            }
            if (errors.Length > 0)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// 保存数据
        /// </summary>
        public void SaveImportData(IEnumerable<Log> personList)
        {
            try
            {
                var _log = new LogDao();
                foreach (var model in personList)
                {
                    var log = new Log();
                    log.Msg = model.Msg;
                    log.Ret = model.Ret;
                    log.CreateTime = model.CreateTime;
                    _log.Add(log);
                }
               
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    
    }
}
