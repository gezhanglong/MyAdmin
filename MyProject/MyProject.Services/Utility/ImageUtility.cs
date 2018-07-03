using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace MyProject.Services.Utility
{
    public class ImageUtility
    {
        /// <summary>
        /// 打水印(文字)
        /// </summary>
        /// <param name="image">原始图片</param>
        /// <param name="x">X位移</param>
        /// <param name="y">Y位移</param>
        /// <param name="text">水印文字</param>
        /// <param name="font">字体</param>
        /// <param name="brush">图形颜色相关</param>
        /// <returns>打水印后的图片</returns>
        public static Image WaterMark(Image image, int x, int y, string text, Font font, Brush brush)
        {
            using (var gWater = Graphics.FromImage(image))
            {
                gWater.DrawString(text, font, brush, x, y);
                gWater.Dispose();
            }
            return image;
        }

        public static Image WaterMark(Image image, string warterMarkPath)
        {
            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(warterMarkPath)))
            {
                //获取水印图片 
                using (var wrImage = Image.FromFile(HttpContext.Current.Server.MapPath(warterMarkPath)))
                {
                    //水印绘制条件：原始图片宽高均大于或等于水印图片 
                    if (image.Width >= wrImage.Width && image.Height >= wrImage.Height)
                    {
                        var gWater = Graphics.FromImage(image);
                        gWater.DrawImage(wrImage,
                                         new Rectangle(image.Width - wrImage.Width,
                                                       image.Height - wrImage.Height, wrImage.Width,
                                                       wrImage.Height), 0, 0, wrImage.Width, wrImage.Height,
                                         GraphicsUnit.Pixel);
                        gWater.Dispose();
                    }
                    wrImage.Dispose();
                }
            }
            return image;
        }

        public static void SaveImage(Image image, string fileName, string savePath)
        {
            var encoderParams = new EncoderParameters();
            var quality = new long[1];
            quality[0] = 100;

            var encoderParam = new EncoderParameter(Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;

            var fileType = Utility.Utils.GetFileExtName(fileName).ToLower();

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
                image.Save(savePath, allICI, encoderParams);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                image.Dispose();
            }
        }

        public static void CutForCustom(HttpPostedFileBase file, string savePath, int width, int height, string watermarkText, string watermarkImage)
        {
            var image = Image.FromStream(file.InputStream, true);
            CutForCustom(image, file.FileName, savePath, width, height, watermarkText, watermarkImage);
        }

        public static void CutForCustom(Image image, string fileName, string savePath, int width, int height, string watermarkText, string watermarkImage)
        {

            // 判断保存路径是否存在
            var dir = Path.GetDirectoryName(savePath);
            if (dir != null && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            // 获得原始图片
            var originalImage = image;
            int x;
            int y;
            int ow;
            int oh;
            var towidth = width;
            var toheight = height;
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

            //新建一个bmp图片
            Image bitmap = new Bitmap(towidth, toheight);

            //新建一个画板
            var g = Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(Color.White);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
                new Rectangle(x, y, ow, oh),
                GraphicsUnit.Pixel);

            //文字水印 
            if (watermarkText != "")
            {
                using (var gWater = Graphics.FromImage(bitmap))
                {
                    var fontWater = new Font("宋体", 10);
                    Brush brushWater = new SolidBrush(Color.White);
                    gWater.DrawString(watermarkText, fontWater, brushWater, 10, 10);
                    gWater.Dispose();
                }
            }

            //透明图片水印 
            if (watermarkImage != "")
            {
                if (System.IO.File.Exists(watermarkImage))
                {
                    //获取水印图片 
                    using (var wrImage = Image.FromFile(watermarkImage))
                    {
                        //水印绘制条件：原始图片宽高均大于或等于水印图片 
                        if (bitmap.Width >= wrImage.Width && bitmap.Height >= wrImage.Height)
                        {
                            var gWater = Graphics.FromImage(bitmap);

                            //透明属性 
                            var imgAttributes = new ImageAttributes();
                            var colorMap = new ColorMap
                                               {
                                                   OldColor = Color.FromArgb(255, 0, 255, 0),
                                                   NewColor = Color.FromArgb(0, 0, 0, 0)
                                               };
                            ColorMap[] remapTable = { colorMap };
                            imgAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                            float[][] colorMatrixElements = {
                                                                    new float[] {1.0f, 0.0f, 0.0f, 0.0f, 0.0f},
                                                                    new float[] {0.0f, 1.0f, 0.0f, 0.0f, 0.0f},
                                                                    new float[] {0.0f, 0.0f, 1.0f, 0.0f, 0.0f},
                                                                    new float[] {0.0f, 0.0f, 0.0f, 0.5f, 0.0f},
                                                                    //透明度:0.5 
                                                                    new float[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f}
                                                                };

                            var wmColorMatrix = new ColorMatrix(colorMatrixElements);
                            imgAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default,
                                                         ColorAdjustType.Bitmap);
                            gWater.DrawImage(wrImage,
                                             new Rectangle(bitmap.Width - wrImage.Width,
                                                           bitmap.Height - wrImage.Height, wrImage.Width,
                                                           wrImage.Height), 10, 10, wrImage.Width, wrImage.Height,
                                             GraphicsUnit.Pixel, imgAttributes);
                            gWater.Dispose();
                        }
                        wrImage.Dispose();
                    }
                }
            }

            var encoderParams = new EncoderParameters();
            var quality = new long[1];
            quality[0] = 100;

            var encoderParam = new EncoderParameter(Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;

            var fileType = Utility.Utils.GetFileExtName(fileName).ToLower();

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
                bitmap.Save(savePath, allICI, encoderParams);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }
    }
}