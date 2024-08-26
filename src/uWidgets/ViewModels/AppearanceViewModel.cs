using System.Diagnostics;
using System.Linq;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media;
using ReactiveUI;
using uWidgets.Core.Interfaces;
using uWidgets.Locales;

namespace uWidgets.ViewModels;

public class AppearanceViewModel(IAppSettingsProvider appSettingsProvider) : ReactiveObject
{
    public ThemeViewModel[] Themes =>
    [
        new ThemeViewModel(
            false, 
            new FontFamily("avares://Avalonia.Fonts.Inter#Inter"),
            !appSettingsProvider.Get().Theme.UseNativeFrame,
            ApplyAppleStyle),
        new ThemeViewModel(
            true, 
            new FontFamily("Segoe UI"),
            appSettingsProvider.Get().Theme.UseNativeFrame,
            ApplyWindowsStyle),
    ];
    
    private void ApplyWindowsStyle()
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
    
    private void ApplyAppleStyle()
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
            desktopApp.Shutdown();
    }
    
    public DarkModeViewModel[] DarkModes =>
    [
        new DarkModeViewModel(Locale.Settings_Appearance_DarkMode_False, false),
        new DarkModeViewModel(Locale.Settings_Appearance_DarkMode_True, true),
        new DarkModeViewModel(Locale.Settings_Appearance_DarkMode_Null, null)
    ];

    public AccentColorViewModel[] AccentComboboxItems =>
    [
        new AccentColorViewModel(Locale.Settings_Appearance_AccentColor_Null, null),
        new AccentColorViewModel(Locale.Settings_Appearance_AccentColor_Manual, "#3376CD")
    ];

    public bool ShowColorPalette => appSettingsProvider.Get().Theme.AccentColor != null;
    
    public AccentColorViewModel AccentMode
    {
        get => appSettingsProvider.Get().Theme.AccentColor == null ? AccentComboboxItems[0] : AccentComboboxItems[1];
        set
        {
            var settings = appSettingsProvider.Get();
            var newTheme = settings.Theme with { AccentColor = value.Value };
            var newSettings = settings with { Theme = newTheme };
            appSettingsProvider.Save(newSettings);
            this.RaisePropertyChanged(nameof(ShowColorPalette));
        }
    }

    public Color AccentColor
    {
        get => Color.TryParse(appSettingsProvider.Get().Theme.AccentColor, out var color) ? color : Colors.DodgerBlue;
        set
        {
            var settings = appSettingsProvider.Get();
            var newTheme = settings.Theme with { AccentColor = value.ToString() };
            var newSettings = settings with { Theme = newTheme };
            appSettingsProvider.Save(newSettings);
        }
    }

    public DarkModeViewModel? DarkMode
    {
        get => DarkModes.FirstOrDefault(theme => theme.Value == appSettingsProvider.Get().Theme.DarkMode);
        set
        {
            var settings = appSettingsProvider.Get();
            var newTheme = settings.Theme with { DarkMode = value?.Value };
            var newSettings = settings with { Theme = newTheme };
            appSettingsProvider.Save(newSettings);
        }
    }

    public bool Transparency
    {
        get => appSettingsProvider.Get().Theme.Transparency;
        set
        {
            var settings = appSettingsProvider.Get();
            var newTheme = settings.Theme with { Transparency = value };
            var newSettings = settings with { Theme = newTheme };
            appSettingsProvider.Save(newSettings);
            this.RaisePropertyChanged();
        }
    }
    
    public double OpacityLevel
    {
        get => appSettingsProvider.Get().Theme.OpacityLevel;
        set
        {
            var settings = appSettingsProvider.Get();
            var newTheme = settings.Theme with { OpacityLevel = value };
            var newSettings = settings with { Theme = newTheme };
            appSettingsProvider.Save(newSettings);
        }
    }
    
    public bool Monochrome
    {
        get => appSettingsProvider.Get().Theme.Monochrome;
        set
        {
            var settings = appSettingsProvider.Get();
            var newTheme = settings.Theme with { Monochrome = value };
            var newSettings = settings with { Theme = newTheme };
            appSettingsProvider.Save(newSettings);
        }
    }
}