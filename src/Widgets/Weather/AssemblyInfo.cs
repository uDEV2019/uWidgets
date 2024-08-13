using System.Reflection;
using uWidgets.Core.Models.Attributes;
using Weather.Locales;
using Weather.Models;
using Weather.Views;
using Weather.Views.Settings;

[assembly: AssemblyCompany("creewick")]
[assembly: AssemblyVersion("1.2.0")]

[assembly: WidgetInfo(typeof(Forecast), typeof(ForecastModel), typeof(ForecastSettings), "Weather_Forecast_Title", "Weather_Forecast_Subtitle")]
[assembly: WidgetInfo(typeof(Temperature), typeof(ForecastModel), typeof(ForecastSettings), "Weather_Temperature_Title", "Weather_Temperature_Subtitle")]
[assembly: WidgetInfo(typeof(UVIndex), typeof(ForecastModel), typeof(ForecastSettings), "Weather_UVIndex_Title", "Weather_UVIndex_Subtitle")]
[assembly: Locale(typeof(Locale), "Weather")]