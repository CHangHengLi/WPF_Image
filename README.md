# WPF Image控件演示程序

## 项目概述

这是一个基于WPF的演示应用程序，旨在展示和说明WPF中Image控件的各种功能和特性。通过这个应用，您可以了解Image控件的基本属性、加载方式、性能优化技巧以及高级应用案例。
![66e33c50-9ce1-4db9-9155-444c82f315d3](https://github.com/user-attachments/assets/6adc4954-5734-48bf-b5e7-e7a9692b14a2)

## 功能特点

- **基本属性演示**：展示Image控件的Source、Stretch、StretchDirection等基本属性的使用
- **图像加载方式**：演示从本地文件、资源、流以及异步方式加载图像
- **效果与变换**：展示如何对图像应用模糊、阴影等效果及缩放、旋转等变换
- **性能优化示例**：演示如何通过DecodePixelWidth/Height、冻结图像等方式优化性能
- **图像处理示例**：展示图像裁剪、合成等高级处理方法

## 系统要求

- .NET 8.0或更高版本
- Windows 10或更高版本

## 使用方法

1. 克隆或下载此项目
2. 使用Visual Studio 2022或更高版本打开解决方案文件
3. 按F5构建并运行项目
4. 通过点击左侧菜单中的不同选项来浏览各种Image控件功能演示

## 项目结构

- `/Images`: 包含演示程序使用的示例图像
- `/Utils`: 包含图像处理的辅助类
- `/Pages`: 包含各种演示页面

## 实现细节

### 1. 图像加载演示

该部分演示了WPF中加载图像的不同方式：
- 从本地文件加载图像
- 异步加载图像以避免UI线程阻塞
- 使用DecodePixelWidth/Height优化内存使用
- 从流中加载图像

这些示例说明了不同场景下的最佳实践，以及如何优化图像加载性能。

### 2. Stretch属性演示

该部分详细展示了Image控件的Stretch属性及其对图像显示的影响：
- None: 保持图像原始尺寸
- Fill: 拉伸图像以填充整个空间（可能会改变图像比例）
- Uniform: 等比例拉伸图像，确保整个图像可见
- UniformToFill: 等比例拉伸图像填满空间，可能裁剪部分图像

同时还演示了StretchDirection属性的使用，包括Both、UpOnly和DownOnly三种设置。

### 3. 特效与变换演示

该部分展示了Image控件可以应用的各种视觉效果：
- 图像效果：模糊、阴影、灰度等
- 图像变换：旋转、缩放、倾斜等
- 图像裁剪：使用几何形状裁剪图像
- 动态效果控制：通过UI动态调整效果参数

### 4. 实用工具类

项目包含一个ImageHelper工具类，提供了多种实用方法：
- 异步加载图像
- 从流中加载图像
- 生成图像缩略图
- 应用灰度效果
- 创建示例DrawingImage

## 核心实现亮点

1. **性能优化技术**
   - 使用DecodePixelWidth/Height减少内存占用
   - 使用CacheOption.OnLoad避免文件锁定
   - 异步加载大图像避免UI阻塞
   - Freeze()方法使图像可以跨线程安全访问

2. **高级功能演示**
   - WriteableBitmap像素级操作
   - 复杂的图像裁剪和变换
   - 结合多种效果的图像处理

3. **代码组织与实践**
   - 遵循MVVM模式的页面设计
   - 可重用的工具类封装
   - 异常处理确保程序稳定性

## 涉及技术

- WPF控件和布局
- 异步图像加载
- 图像效果和变换
- 图像处理技术
- 性能优化

## 相关资源

- [WPF Image类文档](https://learn.microsoft.com/zh-cn/dotnet/api/system.windows.controls.image)
- [WPF图像处理技术](https://learn.microsoft.com/zh-cn/dotnet/desktop/wpf/graphics-multimedia/imaging-overview)

## 许可

本项目仅用于学习和演示目的。
