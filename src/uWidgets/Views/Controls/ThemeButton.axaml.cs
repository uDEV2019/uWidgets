using System;
using System.Diagnostics;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using uWidgets.Core.Interfaces;
using uWidgets.Core.Models.Settings;
using uWidgets.Services;

namespace uWidgets.Views.Controls;

public partial class ThemeButton : UserControl
{
    private static readonly Bitmap wallpaper = GetWallpaperPreview();
    public Theme AppTheme { get; }
    public Bitmap Wallpaper => wallpaper;
    public bool DimWallpaper => AppTheme.DarkMode == true;
    public Brush WidgetBackground => (AppTheme.DarkMode ?? false) switch
    {
        true => new SolidColorBrush(Color.Parse("#2E2E2E"), AppTheme.OpacityLevel),
        false => new SolidColorBrush(Color.Parse("#FFFFFF"), AppTheme.OpacityLevel),
    };
    public CornerRadius WidgetCornerRadius => new(WidgetRadius);
    public double WidgetRadius => AppTheme.UseNativeFrame ? 2 : 10;
    public Thickness WidgetBorderThickness => new(AppTheme.UseNativeFrame ? 1 : 0);
    public BoxShadows WidgetShadow => AppTheme.UseNativeFrame 
        ? new(BoxShadow.Parse("0 0 10 0 #40000000"))
        : new();
    public FontFamily WidgetFontFamily => AppTheme.FontFamily == "Inter"
        ? new FontFamily("avares://Avalonia.Fonts.Inter#Inter")
        : new FontFamily(AppTheme.FontFamily);

    public SolidColorBrush? WidgetForeground => (AppTheme.DarkMode ?? false) switch
    {
        true => new SolidColorBrush((Color)Application.Current!.FindResource("SystemAccentColorLight2")!),
        false => new SolidColorBrush((Color)Application.Current!.FindResource("SystemAccentColorDark1")!),
    };
    
    private readonly IAppSettingsProvider appSettingsProvider;

    public ThemeButton(IAppSettingsProvider appSettingsProvider, Theme appTheme)
    {
        this.appSettingsProvider = appSettingsProvider;
        AppTheme = appTheme;
        DataContext = this;
        InitializeComponent();
    }
    
    public static Bitmap GetWallpaperPreview(int targetHeight = 150)
    {
        var originalBitmap = new Bitmap(InteropService.GetWallpaperPath());

        var aspectRatio = (double)originalBitmap.PixelSize.Width / originalBitmap.PixelSize.Height;
        var targetWidth = (int)(targetHeight * aspectRatio);
        var renderTarget = new RenderTargetBitmap(new PixelSize(targetWidth, targetHeight), new Vector(96, 96));

        using var context = renderTarget.CreateDrawingContext(false);
        var sourceRect = new Rect(0, 0, originalBitmap.PixelSize.Width, originalBitmap.PixelSize.Height);
        var targetRect = new Rect(0, 0, targetWidth, targetHeight);

        context.DrawImage(originalBitmap, sourceRect, targetRect);

        return renderTarget;
    }

    private void Apply(object? sender, RoutedEventArgs e)
    {
        var settings = appSettingsProvider.Get();
        var newSettings = settings with { Theme = AppTheme };
        
        appSettingsProvider.Save(newSettings);
        if (settings.Theme.UseNativeFrame != newSettings.Theme.UseNativeFrame)
            Restart();
    }
    
    private void Restart()
    {
        var executablePath = Process.GetCurrentProcess().MainModule?.FileName;
        if (executablePath == null) return;
        
        var process = Process.Start(executablePath, "--settings");
        var tryCount = 0;
        var maxTryCount = 10;
        
        while (process.MainWindowHandle == IntPtr.Zero && !process.HasExited && tryCount++ < maxTryCount)
        {
            Thread.Sleep(100);
            process.Refresh();
        }

        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopApp) 
            desktopApp.Shutdown();
    }
}