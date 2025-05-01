using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WPF_Image.Utils;

namespace WPF_Image.Pages
{
    /// <summary>
    /// LoadingDemoPage.xaml 的交互逻辑
    /// </summary>
    public partial class LoadingDemoPage : Page
    {
        private readonly string _sampleImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "sample.jpg");
        
        public LoadingDemoPage()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// 从本地文件加载图像
        /// </summary>
        private void btnLoadLocal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtLocalStatus.Text = "加载中...";
                
                // 使用BitmapImage加载本地图像
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(_sampleImagePath);
                bitmap.CacheOption = BitmapCacheOption.OnLoad; // 加载完成后关闭流
                bitmap.EndInit();
                
                imgLocal.Source = bitmap;
                txtLocalStatus.Text = "加载完成";
            }
            catch (Exception ex)
            {
                txtLocalStatus.Text = "加载失败";
                MessageBox.Show($"加载图像失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        /// <summary>
        /// 异步加载图像
        /// </summary>
        private async void btnLoadAsync_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 重置界面状态
                imgAsync.Source = null;
                progressAsync.Visibility = Visibility.Visible;
                txtAsyncStatus.Text = "正在异步加载...";
                
                // 使用异步方法加载图像
                BitmapImage bitmap = await ImageHelper.LoadImageAsync(_sampleImagePath);
                
                // 设置图像源
                imgAsync.Source = bitmap;
                txtAsyncStatus.Text = "加载完成";
            }
            catch (Exception ex)
            {
                txtAsyncStatus.Text = "加载失败";
                MessageBox.Show($"异步加载图像失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                progressAsync.Visibility = Visibility.Collapsed;
            }
        }
        
        /// <summary>
        /// 加载原始大小图像（不优化）
        /// </summary>
        private void btnLoadOriginal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 加载原始大小的图像
                long beforeMemory = GC.GetTotalMemory(true);
                
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(_sampleImagePath);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                
                imgOriginal.Source = bitmap;
                
                long afterMemory = GC.GetTotalMemory(false);
                long memoryUsed = (afterMemory - beforeMemory) / 1024; // 转换为KB
                
                txtMemoryUsage.Text = $"原始大小内存使用: {memoryUsed} KB";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载原始图像失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        /// <summary>
        /// 加载优化大小图像
        /// </summary>
        private void btnLoadOptimized_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 加载优化大小的图像
                long beforeMemory = GC.GetTotalMemory(true);
                
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(_sampleImagePath);
                bitmap.DecodePixelWidth = 250; // 设置解码宽度为显示宽度
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                
                imgOptimized.Source = bitmap;
                
                long afterMemory = GC.GetTotalMemory(false);
                long memoryUsed = (afterMemory - beforeMemory) / 1024; // 转换为KB
                
                txtMemoryUsage.Text += $"，优化大小内存使用: {memoryUsed} KB";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载优化图像失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        /// <summary>
        /// 从流加载图像
        /// </summary>
        private void btnLoadFromStream_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtStreamStatus.Text = "从流加载中...";
                
                // 打开文件流
                using (FileStream stream = new FileStream(_sampleImagePath, FileMode.Open, FileAccess.Read))
                {
                    // 使用工具类从流加载图像
                    BitmapImage bitmap = ImageHelper.LoadImageFromStream(stream);
                    imgStream.Source = bitmap;
                }
                
                txtStreamStatus.Text = "加载完成";
            }
            catch (Exception ex)
            {
                txtStreamStatus.Text = "加载失败";
                MessageBox.Show($"从流加载图像失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
} 