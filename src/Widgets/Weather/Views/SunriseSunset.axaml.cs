using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Weather.Models;
using Weather.ViewModels;

namespace Weather.Views;

public partial class SunriseSunset : UserControl
{
    public SunriseSunset() : this(new ForecastModel("Cupertino", 37.3230, -122.0322, "celsius")) {}

    public SunriseSunset(ForecastModel model)
    {
        DataContext = new ForecastViewModel(model);
        InitializeComponent();
    }
}