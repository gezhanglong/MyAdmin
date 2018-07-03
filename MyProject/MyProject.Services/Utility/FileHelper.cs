using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyProject.Services.Utility
{
    public class FileHelper
    {
        /// <summary>
        /// 读取目标文件夹下的子文件夹列表
        /// </summary>
        /// <param name="dirPath"></param>
        /// <returns></returns>
        public static List<string> GetDirList(string dirPath)
        {
            return Directory.GetDirectories(dirPath).ToList();
        }

        /// <summary>
        /// 读取目录文件夹下的文件列表
        /// </summary>
        /// <param name="dirPath"></param>
        /// <returns></returns>
        public static List<string> GetFiles(string dirPath)
        {
            return Directory.GetFiles(dirPath).ToList();
        }

        public static string GetServerPath(string path)
        {
            return AppDomain.CurrentDomain.BaseDirectory + path;
        }

        public static void CreateDir(string dirPath)
        {
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
        }

        public static string GetDirName(string dirPath)
        {
            return string.IsNullOrEmpty(dirPath)
                       ? string.Empty
                       : dirPath.Substring(dirPath.LastIndexOf("/", System.StringComparison.Ordinal) + 1,
                                           dirPath.Length - dirPath.LastIndexOf("/", System.StringComparison.Ordinal) -
                                           1);
        }

        public static string GetFileName(string path)
        {
            return string.IsNullOrEmpty(path)
                       ? string.Empty
                       : Utils.GetFilename(path);
        }

        public static bool IsImage(string extension)
        {
            const string imageExtension = ".jpg,.jpeg,.bmp,.gif,.png";
            return imageExtension.Contains(extension);
        }
    }
}