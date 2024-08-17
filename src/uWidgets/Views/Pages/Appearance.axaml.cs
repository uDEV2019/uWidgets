using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using uWidgets.Core.Interfaces;
using uWidgets.ViewModels;

namespace uWidgets.Views.Pages;

public partial class Appearance : UserControl
{
    private readonly IAppSettingsProvider appSettingsProvider;

    public Appearance(IAppSettingsProvider appSettingsProvider)
    {
        this.appSettingsProvider = appSettingsProvider;
        DataContext = new AppearanceViewModel(appSettingsProvider);
        InitializeComponent();
    }

    private void ApplyWindowsStyle(object? sender, RoutedEventArgs e)
    {
        var settings = appSettingsProvider.Get();
        var newSettings = settings with
        {
            Theme = settings.Theme with
            {
                UseNativeFrame = true,
                FontFamily = "Segoe UI"
            },
            Dimensions = settings.Dimensions with
            {
                Radius = 0,
            },
        };
        
        appSettingsProvider.Save(newSettings);
        Restart();
    }

    private void ApplyAppleStyle(object? sender, RoutedEventArgs e)
    {
        var settings = appSettingsProvider.Get();
        var newSettings = settings with
        {
            Theme = settings.Theme with
            {
                UseNativeFrame = false,
                FontFamily = "Inter"
            },
            Dimensions = settings.Dimensions with
            {
                Radius = 24,
            },
        };
        
        appSettingsProvider.Save(newSettings);
        Restart();
    }
    
    private void Restart()
    {
        var executablePath = Process.GetCurrentProcess().MainModule?.FileName;
        if (executablePath == null) return;
        
        Process.Start(executablePath);
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopApp)
        {
            desktopApp.Shutdown();
        }
    }
}