using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WPF_Image.Pages
{
    /// <summary>
    /// StretchDemoPage.xaml 的交互逻辑
    /// </summary>
    public partial class StretchDemoPage : Page
    {
        private readonly string _sampleImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "sample.jpg");
        private readonly string _logoImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "logo .png");
        
        public StretchDemoPage()
        {
            InitializeComponent();
            LoadImages();
        }
        
        /// <summary>
        /// 加载演示图像
        /// </summary>
        private void LoadImages()
        {
            try
            {
                // 加载样本图像作为演示
                BitmapImage sampleBitmap = new BitmapImage(new Uri(_sampleImagePath));
                
                // 设置Stretch演示区域的图像
                imgNone.Source = sampleBitmap;
                imgFill.Source = sampleBitmap;
                imgUniform.Source = sampleBitmap;
                imgUniformToFill.Source = sampleBitmap;
                
                imgNoneWide.Source = sampleBitmap;
                imgFillWide.Source = sampleBitmap;
                imgUniformWide.Source = sampleBitmap;
                imgUniformToFillWide.Source = sampleBitmap;
                
                // 加载Logo图像用于StretchDirection演示
                // 注：Logo图像通常较小，更适合演示StretchDirection
                BitmapImage logoBitmap = new BitmapImage(new Uri(_logoImagePath));
                
                // 设置StretchDirection演示区域的图像
                imgBoth.Source = logoBitmap;
                imgUpOnly.Source = logoBitmap;
                imgDownOnly.Source = logoBitmap;
                
                // 设置对齐方式演示区域的图像
                imgLeft.Source = sampleBitmap;
                imgCenter.Source = sampleBitmap;
                imgRight.Source = sampleBitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载图像失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
} 