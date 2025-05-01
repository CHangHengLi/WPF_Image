using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPF_Image.Utils
{
    /// <summary>
    /// 提供图像处理的辅助方法
    /// </summary>
    public static class ImageHelper
    {
        /// <summary>
        /// 异步加载图像
        /// </summary>
        /// <param name="imagePath">图像路径</param>
        /// <returns>加载的BitmapImage对象</returns>
        public static async Task<BitmapImage> LoadImageAsync(string imagePath)
        {
            return await Task.Run(() =>
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imagePath);
                bitmap.CacheOption = BitmapCacheOption.OnLoad; // 加载完成后关闭流
                bitmap.EndInit();
                bitmap.Freeze(); // 使图像可以跨线程访问
                return bitmap;
            });
        }

        /// <summary>
        /// 从流中加载图像
        /// </summary>
        /// <param name="imageStream">图像流</param>
        /// <returns>加载的BitmapImage对象</returns>
        public static BitmapImage LoadImageFromStream(Stream imageStream)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = imageStream;
            bitmap.CacheOption = BitmapCacheOption.OnLoad; // 图像加载后关闭流
            bitmap.EndInit();
            return bitmap;
        }

        /// <summary>
        /// 生成图像的缩略图
        /// </summary>
        /// <param name="originalImagePath">原始图像路径</param>
        /// <param name="thumbnailSize">缩略图尺寸</param>
        /// <returns>缩略图的BitmapSource</returns>
        public static BitmapSource GenerateThumbnail(string originalImagePath, int thumbnailSize)
        {
            BitmapImage originalImage = new BitmapImage();
            originalImage.BeginInit();
            originalImage.UriSource = new Uri(originalImagePath);
            originalImage.DecodePixelWidth = thumbnailSize; // 只需设置宽度，高度会按比例缩放
            originalImage.CacheOption = BitmapCacheOption.OnLoad;
            originalImage.EndInit();
            return originalImage;
        }

        /// <summary>
        /// 将缩略图保存为文件
        /// </summary>
        /// <param name="image">图像源</param>
        /// <param name="filePath">保存路径</param>
        public static void SaveThumbnailToFile(BitmapSource image, string filePath)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.QualityLevel = 80; // 控制JPEG质量级别
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(stream);
            }
        }
        
        /// <summary>
        /// 创建一个示例DrawingImage
        /// </summary>
        /// <returns>一个DrawingImage对象</returns>
        public static DrawingImage CreateSampleDrawingImage()
        {
            DrawingImage drawingImage = new DrawingImage();
            DrawingGroup drawingGroup = new DrawingGroup();
            
            using (DrawingContext dc = drawingGroup.Open())
            {
                // 绘制背景
                dc.DrawRectangle(
                    new LinearGradientBrush(
                        Colors.LightBlue, Colors.WhiteSmoke, 
                        new Point(0, 0), new Point(1, 1)),
                    null,
                    new Rect(0, 0, 200, 150));
                
                // 绘制一些示例图形
                dc.DrawEllipse(
                    Brushes.LightCoral,
                    new Pen(Brushes.Red, 2),
                    new Point(70, 60),
                    30, 30);
                
                dc.DrawRectangle(
                    Brushes.LightGreen,
                    new Pen(Brushes.Green, 2),
                    new Rect(110, 30, 60, 60));
                
                // 添加一些文字
                FormattedText text = new FormattedText(
                    "Sample",
                    System.Globalization.CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    new Typeface("Segoe UI"),
                    16,
                    Brushes.DarkBlue,
                    VisualTreeHelper.GetDpi(new Window()).PixelsPerDip);
                
                dc.DrawText(text, new Point(70, 110));
            }
            
            drawingImage.Drawing = drawingGroup;
            return drawingImage;
        }
        
        /// <summary>
        /// 应用灰度效果到BitmapSource
        /// </summary>
        /// <param name="source">原始图像</param>
        /// <returns>应用灰度效果后的图像</returns>
        public static BitmapSource ApplyGrayscaleEffect(BitmapSource source)
        {
            // 创建可写位图
            WriteableBitmap target = new WriteableBitmap(source);
            
            // 锁定位图以进行写入
            target.Lock();
            
            try
            {
                unsafe
                {
                    // 获取后备缓冲区
                    IntPtr backBuffer = target.BackBuffer;
                    int stride = target.BackBufferStride;
                    
                    // 遍历所有像素
                    for (int y = 0; y < target.PixelHeight; y++)
                    {
                        for (int x = 0; x < target.PixelWidth; x++)
                        {
                            // 计算像素偏移量
                            int offset = y * stride + x * 4;
                            
                            // 读取像素值
                            byte blue = *(byte*)(backBuffer + offset);
                            byte green = *(byte*)(backBuffer + offset + 1);
                            byte red = *(byte*)(backBuffer + offset + 2);
                            
                            // 计算灰度值 (0.299R + 0.587G + 0.114B)
                            byte gray = (byte)(0.299 * red + 0.587 * green + 0.114 * blue);
                            
                            // 设置新的像素值
                            *(byte*)(backBuffer + offset) = gray;     // B
                            *(byte*)(backBuffer + offset + 1) = gray; // G
                            *(byte*)(backBuffer + offset + 2) = gray; // R
                            // Alpha通道保持不变
                        }
                    }
                    
                    // 通知位图更改
                    target.AddDirtyRect(new Int32Rect(0, 0, target.PixelWidth, target.PixelHeight));
                }
            }
            finally
            {
                // 解锁位图
                target.Unlock();
            }
            
            return target;
        }
    }
} 