using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Effects;

namespace WPF_Image
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadWelcomeImage();
        }

        // 加载欢迎页面的图像
        private void LoadWelcomeImage()
        {
            try
            {
                // 创建一个简单的DrawingImage作为欢迎图像
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
                        new Rect(0, 0, 300, 200));
                    
                    // 绘制一些示例图形
                    dc.DrawEllipse(
                        Brushes.LightCoral,
                        new Pen(Brushes.Red, 2),
                        new Point(100, 80),
                        40, 40);
                    
                    dc.DrawRectangle(
                        Brushes.LightGreen,
                        new Pen(Brushes.Green, 2),
                        new Rect(160, 40, 80, 80));
                    
                    // 添加一些文字
                    FormattedText text = new FormattedText(
                        "Image Demo",
                        System.Globalization.CultureInfo.CurrentCulture,
                        FlowDirection.LeftToRight,
                        new Typeface("Segoe UI"),
                        24,
                        Brushes.DarkBlue,
                        VisualTreeHelper.GetDpi(this).PixelsPerDip);
                    
                    dc.DrawText(text, new Point(80, 140));
                }
                
                drawingImage.Drawing = drawingGroup;
                welcomeImage.Source = drawingImage;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"加载欢迎图像时出错: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // 基本属性演示按钮点击事件
        private void btnBasicDemo_Click(object sender, RoutedEventArgs e)
        {
            ClearContent();
            
            // 创建基本属性演示内容
            StackPanel panel = new StackPanel { Margin = new Thickness(10) };
            
            // 添加标题
            panel.Children.Add(new TextBlock 
            { 
                Text = "基本属性演示", 
                FontSize = 20, 
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 0, 0, 20)
            });
            
            // 添加Source属性说明
            panel.Children.Add(new TextBlock 
            { 
                Text = "Source属性: 指定要显示的图像源", 
                FontSize = 16,
                Margin = new Thickness(0, 0, 0, 10)
            });
            
            // 创建一个简单的BitmapSource作为示例
            DrawingImage simpleImage = CreateSampleDrawingImage();
            
            Image imageControl = new Image
            {
                Source = simpleImage,
                Width = 200,
                Height = 150,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(0, 0, 0, 20)
            };
            
            panel.Children.Add(imageControl);
            
            // 添加其他属性说明
            panel.Children.Add(new TextBlock 
            { 
                Text = "其他常用属性:",
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 10, 0, 5)
            });
            
            string properties = 
                "- Stretch: 控制图像如何填充分配的空间\n" +
                "- StretchDirection: 控制图像的拉伸方向\n" +
                "- HorizontalAlignment/VerticalAlignment: 控制图像在容器中的对齐方式\n" +
                "- Width/Height: 控制图像控件的尺寸";
                
            panel.Children.Add(new TextBlock 
            { 
                Text = properties,
                TextWrapping = TextWrapping.Wrap
            });
            
            contentArea.Children.Add(panel);
            statusText.Text = "显示基本属性演示";
        }

        // Stretch属性演示按钮点击事件
        private void btnStretchDemo_Click(object sender, RoutedEventArgs e)
        {
            ClearContent();
            
            try
            {
                // 加载Stretch属性演示页面
                var stretchDemoPage = new Pages.StretchDemoPage();
                
                // 创建一个Frame来承载页面
                Frame frame = new Frame();
                frame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
                frame.Content = stretchDemoPage;
                
                contentArea.Children.Add(frame);
                statusText.Text = "显示Stretch属性演示";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载页面失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        // 特效和变换演示按钮点击事件
        private void btnEffectsDemo_Click(object sender, RoutedEventArgs e)
        {
            ClearContent();
            
            try
            {
                // 加载特效和变换演示页面
                var effectsDemoPage = new Pages.EffectsDemoPage();
                
                // 创建一个Frame来承载页面
                Frame frame = new Frame();
                frame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
                frame.Content = effectsDemoPage;
                
                contentArea.Children.Add(frame);
                statusText.Text = "显示特效和变换演示";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载页面失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        // 加载方式演示按钮点击事件
        private void btnLoadingDemo_Click(object sender, RoutedEventArgs e)
        {
            ClearContent();
            
            try
            {
                // 加载加载方式演示页面
                var loadingDemoPage = new Pages.LoadingDemoPage();
                
                // 创建一个Frame来承载页面
                Frame frame = new Frame();
                frame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
                frame.Content = loadingDemoPage;
                
                contentArea.Children.Add(frame);
                statusText.Text = "显示加载方式演示";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载页面失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // 创建列表项
        private UIElement CreateListItem(string text)
        {
            TextBlock textBlock = new TextBlock
            {
                Text = "• " + text,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(10, 3, 0, 3)
            };
            
            return textBlock;
        }

        // 清除内容区域
        private void ClearContent()
        {
            contentArea.Children.Clear();
        }
        
        // 在网格中添加文本块
        private void AddTextBlock(Grid grid, string text, int row, int column)
        {
            TextBlock textBlock = new TextBlock
            {
                Text = text,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(5)
            };
            
            Grid.SetRow(textBlock, row);
            Grid.SetColumn(textBlock, column);
            
            grid.Children.Add(textBlock);
        }
        
        // 在网格中添加带边框的图像
        private void AddImageWithBorder(Grid grid, ImageSource source, Stretch stretch, int row, int column)
        {
            Border border = new Border
            {
                BorderBrush = Brushes.LightGray,
                BorderThickness = new Thickness(1),
                Width = 120,
                Height = 80,
                Margin = new Thickness(5)
            };
            
            Image image = new Image
            {
                Source = source,
                Stretch = stretch
            };
            
            border.Child = image;
            
            Grid.SetRow(border, row);
            Grid.SetColumn(border, column);
            
            grid.Children.Add(border);
        }
        
        // 创建示例DrawingImage
        private DrawingImage CreateSampleDrawingImage()
        {
            DrawingImage drawingImage = new DrawingImage();
            DrawingGroup drawingGroup = new DrawingGroup();
            
            using (DrawingContext dc = drawingGroup.Open())
            {
                // 绘制背景
                dc.DrawRectangle(
                    Brushes.LightBlue,
                    null,
                    new Rect(0, 0, 100, 100));
                
                // 绘制一些形状
                dc.DrawEllipse(
                    Brushes.Yellow,
                    new Pen(Brushes.Orange, 2),
                    new Point(30, 30),
                    20, 20);
                
                dc.DrawRectangle(
                    Brushes.LightGreen,
                    new Pen(Brushes.Green, 2),
                    new Rect(60, 20, 30, 30));
                
                // 绘制一条线
                dc.DrawLine(
                    new Pen(Brushes.Red, 3),
                    new Point(10, 50),
                    new Point(90, 90));
            }
            
            drawingImage.Drawing = drawingGroup;
            return drawingImage;
        }
    }
}