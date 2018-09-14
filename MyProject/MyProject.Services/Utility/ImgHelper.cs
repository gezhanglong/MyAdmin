using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Web;

namespace MyProject.Services.Utility
{
    /// <summary>
    /// 图片缩量方式
    /// </summary>
    public enum ImgModel
    {
        /// <summary>
        /// 指定高宽缩放（可能变形） 
        /// </summary>
        Hw,
        /// <summary>
        /// 指定宽，高按比例   
        /// </summary>
        W,
        /// <summary>
        /// 指定高，宽按比例
        /// </summary>
        H,
        /// <summary>
        /// 指定高宽裁减（不变形）
        /// </summary>
        Cut

    }

    /// <summary>
    /// 图片帮助类
    /// </summary>
    public class ImgHelper
    {

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>    
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                
                    break;
                case "W"://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    else
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(Color.White);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
                new System.Drawing.Rectangle(x, y, ow, oh),
                System.Drawing.GraphicsUnit.Pixel);

            var encoderParams = new EncoderParameters();
            long[] quality = new long[1];
            quality[0] = 100;

            var encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;

            var lenEnd = originalImagePath.LastIndexOf('.');
            var fileType = originalImagePath.Substring(lenEnd, originalImagePath.Length - lenEnd);

            ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo jpegICI = null;
            ImageCodecInfo GIFICI = null;
            ImageCodecInfo jpgICI = null;
            ImageCodecInfo pngICI = null;
            ImageCodecInfo bmpICI = null;

            ImageCodecInfo allICI = null;
            foreach (var t in arrayICI)
            {
                if (t.FormatDescription.Equals("JPEG"))
                    jpegICI = t;//设置JPEG编码

                if (t.FormatDescription.Equals("GIF"))
                    GIFICI = t;//设置GIF编码

                if (t.FormatDescription.Equals("JPG"))
                    jpgICI = t;//设置GIF编码

                if (t.FormatDescription.Equals("PNG"))
                    pngICI = t;//设置GIF编码

                if (t.FormatDescription.Equals("BMP"))
                    bmpICI = t;//设置GIF编码
            }

            switch (fileType.ToLower())
            {
                case ".jpg":
                    allICI = jpegICI;
                    break;
                case ".gif":
                    allICI = GIFICI;
                    break;
                case ".jpeg":
                    allICI = jpegICI;
                    break;
                case ".bmp":
                    allICI = bmpICI;
                    break;
                case ".png":
                    allICI = pngICI;
                    break;
            }

            try
            {
                bitmap.Save(thumbnailPath, allICI, encoderParams);
                ////以jpg格式保存缩略图
                //bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }

        ///// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImageStream">源图字节</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="model">图片缩量方式</param>
        /// <param name="imgFomat">图片类型</param>    
        public static byte[] MakeThumbnail(Stream originalImageStream, int width, int height, ImgModel model, ImageFormat imgFomat)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromStream(originalImageStream);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (model)
            {
                case ImgModel.Hw://指定高宽缩放（可能变形）                
                    break;
                case ImgModel.W://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case ImgModel.H://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case ImgModel.Cut://指定高宽裁减（不变形）       
                    oh = originalImage.Height;
                    ow = originalImage.Width;
                    x = 0;
                    y = 0;
                    //if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    //{
                    //    oh = originalImage.Height;
                    //    ow = originalImage.Height * towidth / toheight;
                    //    y = 0;
                    //    x = (originalImage.Width - ow) / 2;
                    //}
                    //else
                    //{
                    //    ow = originalImage.Width;
                    //    oh = originalImage.Width * height / towidth;
                    //    x = 0;
                    //    y = (originalImage.Height - oh);
                    //}
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(System.Drawing.Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
                new System.Drawing.Rectangle(x, y, ow, oh),
                System.Drawing.GraphicsUnit.Pixel);

            try
            {
                //以指定格式保存缩略图 
                return ImageToByteArray(bitmap, imgFomat);
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }


        /// <summary>
        /// 图片转换成字节
        /// </summary>
        /// <param name="imageIn">图片Images对象</param>
        /// <param name="model">图片转换类型</param>
        /// <returns>返回转换后字节</returns>
        public static byte[] ImageToByteArray(System.Drawing.Image imageIn, ImageFormat model)
        {
            var ms = new MemoryStream();
            imageIn.Save(ms, model);
            return ms.ToArray();
        }

        /// <summary>
        /// 替换文件后缀
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <param name="replace">替换的后缀名</param>
        /// <returns>返回替换后的文件名</returns>
        public static string ReplaceSuffix(string filename, string replace)
        {
            var oldValue = filename.Substring(filename.IndexOf('.') + 1);

            return filename.Replace(oldValue, replace);
        }

        /// <summary>
        /// 字节转换成图片
        /// </summary>
        /// <param name="byteArrayIn">图片字节</param>
        /// <returns>返回图片Images对象</returns>
        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            var ms = new MemoryStream(byteArrayIn);
            var returnImage = Image.FromStream(ms);
            return returnImage;

        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="files"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string UploadImag(HttpFileCollectionBase files, ref string msg)
        {
            var backPath = "";
            if (files.Count <= 0)
            {
                msg = "请选择要上传的图片";
                return backPath;
            }
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase file = files[i];
                if (file.ContentType.Contains("image/"))
                {
                    using (System.Drawing.Image img = System.Drawing.Image.FromStream(file.InputStream))
                    {
                        string FileName = System.IO.Path.GetFileName(file.FileName);
                        string[] SplitFileName = FileName.Split('.');
                        string AtterFileName = DateTime.Now.ToString("yyyMMddHHmmss") + "." + SplitFileName[1];
                        backPath = "/Content/UploadImg/" + AtterFileName;
                        img.Save(System.Web.HttpContext.Current.Server.MapPath("/Content/UploadImg/" + AtterFileName));
                    }
                }
                else
                {

                    msg = "该文件不是图片格式！";
                    return backPath;
                } 
            }
            msg = "上传成功";
            return backPath;
        }

        /// <summary>
        /// 带文字水印图片上传
        /// </summary>
        /// <param name="files"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string UploadImagFont(HttpFileCollectionBase files, ref string msg)
        {
            var backPath = "";
            if (files.Count <= 0)
            {
                msg = "请选择要上传的图片";
                return backPath;
            }
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase file = files[i]; 
                    if (file.ContentType.Contains("image/"))
                    {
                        using (System.Drawing.Image img = System.Drawing.Image.FromStream(file.InputStream))
                        {
                            using (Graphics g = Graphics.FromImage(img))
                            {
                                g.DrawString("我的图片", new Font("宋体", 14), Brushes.Red, 0, 0);
                            }
                            string FileName = System.IO.Path.GetFileName(file.FileName);
                            string[] SplitFileName = FileName.Split('.');
                            string AtterFileName = DateTime.Now.ToString("yyyMMddHHmmss") + "." + SplitFileName[1];
                            backPath = "/Content/UploadImg/" + AtterFileName;
                            img.Save(System.Web.HttpContext.Current.Server.MapPath("/Content/UploadImg/" + AtterFileName));
                        }
                    } 
                else
                {
                    msg = "请选择要上传的图片！";
                    return backPath;
                }

            } 
            msg = "上传成功";
            return backPath;
        }


        /// <summary>
        ///  带图片水印图片上传
        /// </summary>
        /// <param name="files"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string  UploadImagImg(HttpFileCollectionBase files, ref string msg)
        {
            var backPath = "";
            if (files.Count <= 0)
            {
                msg = "请选择要上传的图片";
                return backPath;
            }
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase file = files[i];
                if (file.ContentType.Contains("image/"))
                {
                    string fileName = file.FileName;
                         using (System.Drawing.Image img = System.Drawing.Image.FromStream(file.InputStream))
                         {
                             using (System.Drawing.Image imgWater = System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath("/App_Data/20180730142135.png")))
                             {
                                 using (Graphics g = Graphics.FromImage(img))
                                 {
                                     g.DrawImage(imgWater, 0, 0);
                                 }
                                 string[] SplitFileName = fileName.Split('.');
                                 string AtterFileName = DateTime.Now.ToString("yyyMMddHHmmss") + "." + SplitFileName[1];
                                 backPath = "/Content/UploadImg/" + AtterFileName;
                                 img.Save(System.Web.HttpContext.Current.Server.MapPath("/Content/UploadImg/" + AtterFileName));
                             }
                         }
                }
                else
                {
                    msg = "请选择要上传的图片！";
                    return backPath;
                }

            }
            msg = "上传成功";
            return backPath;
        }

        /// <summary>
        /// 缩略图上传
        /// </summary>
        /// <param name="files"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string  UploadImagBitmap(HttpFileCollectionBase files, ref string msg)
        {
            var backPath = "";
            if (files.Count <= 0)
            {
                msg = "请选择要上传的图片";
                return backPath;
            }
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase file = files[i];
                if (file.ContentType.Contains("image/"))
                {
                    using (System.Drawing.Image img = System.Drawing.Image.FromStream(file.InputStream))
                    {
                        using (System.Drawing.Image imgThumb = new Bitmap(200, 100))
                        {
                            using (Graphics g = Graphics.FromImage(imgThumb))
                            {
                                g.DrawImage(img, new Rectangle(0, 0, imgThumb.Width, imgThumb.Height), new Rectangle(0, 0, img.Width, img.Height), GraphicsUnit.Pixel);
                            }
                            string fileName = file.FileName;
                            string[] SplitFileName = fileName.Split('.');
                            string AtterFileName = DateTime.Now.ToString("yyyMMddHHmmss") + "." + SplitFileName[1];
                            backPath = "/Content/UploadImg/" + AtterFileName;
                            img.Save(System.Web.HttpContext.Current.Server.MapPath("/Content/UploadImg/" + AtterFileName));
                        }
                    }
                }
                else
                {
                    msg = "请选择要上传的图片！";
                    return backPath;
                }

            }
            msg = "上传成功";
            return backPath;
        }


        /// <summary>
        /// 本地图片地址转二进制
        /// </summary>
        /// <param name="imagepath"></param>
        /// <returns></returns>
        public static byte[] GetPictureData(string imagepath)
        { 
            ////根据图片文件的路径使用文件流打开，并保存为byte[] 
            FileStream fs = new FileStream(imagepath, FileMode.Open);//可以是其他重载方法 
            byte[] byData = new byte[fs.Length];
            fs.Read(byData, 0, byData.Length);
            fs.Close();
            return byData;
        }

        /// <summary>
        /// 网络图片地址转二进制
        /// </summary>
        /// <param name="imageNetUrl">网络地址：如http://pic.lkgame.com/Upload/WebGame/Images/201809/20180907092323325.jpg</param>
        /// <returns></returns>
        public static byte[] getPicData(string imageNetUrl)
        { 
            byte[] bytes;
            WebRequest request = WebRequest.Create(imageNetUrl);

            using (WebResponse response = request.GetResponse())
            {
                MemoryStream memoryStream = new MemoryStream();
                const int bufferLength = 1024;
                int actual;
                byte[] buffer = new byte[bufferLength];
                while ((actual = response.GetResponseStream().Read(buffer, 0, bufferLength)) > 0)
                {
                    memoryStream.Write(buffer, 0, actual);
                }
                memoryStream.Position = 0;

                bytes = new byte[memoryStream.Length];
                memoryStream.Read(bytes, 0, bytes.Length);
                // 设置当前流的位置为流的开始
                memoryStream.Seek(0, SeekOrigin.Begin);
                memoryStream.Close();
            }
            return bytes;
        }
       
    }
}
