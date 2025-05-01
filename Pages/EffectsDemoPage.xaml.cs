using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using WPF_Image.Utils;

namespace WPF_Image.Pages
{
    /// <summary>
    /// EffectsDemoPage.xaml 的交互逻辑
    /// </summary>
    public partial class EffectsDemoPage : Page
    {
        private readonly string _sampleImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "sample.jpg");
        private BitmapImage? _originalBitmap;
        
        public EffectsDemoPage()
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
                _originalBitmap = new BitmapImage(new Uri(_sampleImagePath));
                
                // 设置各个图像控件的Source
                imgOriginal.Source = _originalBitmap;
                imgBlur.Source = _originalBitmap;
                imgShadow.Source = _originalBitmap;
                imgGrayscale.Source = _originalBitmap;
                
                imgTransformOriginal.Source = _originalBitmap;
                imgRotate.Source = _originalBitmap;
                imgScale.Source = _originalBitmap;
                imgCombined.Source = _originalBitmap;
                
                imgClipOriginal.Source = _originalBitmap;
                imgRectClip.Source = _originalBitmap;
                imgEllipseClip.Source = _originalBitmap;
                imgPathClip.Source = _originalBitmap;
                
                imgDynamicBlur.Source = _originalBitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载图像失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        /// <summary>
        /// 应用灰度效果按钮点击事件
        /// </summary>
        private void btnApplyGrayscale_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_originalBitmap == null)
                {
                    MessageBox.Show("原始图像未加载，无法应用灰度效果");
                    return;
                }
                
                // 使用工具类应用灰度效果
                BitmapSource grayscaleImage = ImageHelper.ApplyGrayscaleEffect(_originalBitmap);
                imgGrayscale.Source = grayscaleImage;
                
                // 禁用按钮，防止重复点击
                btnApplyGrayscale.IsEnabled = false;
                btnApplyGrayscale.Content = "已应用灰度效果";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"应用灰度效果失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        /// <summary>
        /// 模糊滑块值改变事件
        /// </summary>
        private void sliderBlur_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // 更新文本显示
            if (txtBlurValue != null)
            {
                txtBlurValue.Text = sliderBlur.Value.ToString("0");
            }
            
            // 更新模糊效果
            if (imgDynamicBlur != null && imgDynamicBlur.Effect is BlurEffect blurEffect)
            {
                blurEffect.Radius = sliderBlur.Value;
            }
        }
    }
} 