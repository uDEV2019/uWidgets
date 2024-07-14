using System.Linq;
using Avalonia;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using uWidgets.Core.Interfaces;
using uWidgets.Core.Services;
using uWidgets.Services;
using uWidgets.Views;

namespace uWidgets;

public class App : Application
{
    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        var services = new ServiceCollection()
            .AddSingleton<IAppSettingsProvider, AppSettingsProvider>()
            .AddSingleton<ILayoutProvider, LayoutProvider>()
            .AddSingleton<IAssemblyProvider, AssemblyProvider>()
            .AddSingleton<IThemeService, ThemeService>()
            .AddSingleton<ILocaleService, LocaleService>()
            .AddSingleton<IGridService<Widget>, GridService>()
            .AddSingleton<IWidgetFactory<Widget>, WidgetFactory>()
            .AddSingleton<Settings, Settings>()
            .BuildServiceProvider();

        var appSettingsProvider = services
            .GetRequiredService<IAppSettingsProvider>();

        var themeService = services
            .GetRequiredService<IThemeService>();
        
        var localeService = services
            .GetRequiredService<ILocaleService>();
        
        localeService.SetCulture(appSettingsProvider.Get().Region.Language);
        themeService.Apply(appSettingsProvider.Get().Theme);

        var widgets = services
            .GetRequiredService<IWidgetFactory<Widget>>()
            .Create()
            .ToList();

        foreach (var widget in widgets) 
            widget.Show();

        if (widgets.Count == 0)
            services.GetRequiredService<Settings>().Show();
        
        base.OnFrameworkInitializationCompleted();
    }
}