using System.Reflection;
using Monitor.Locales;
using Monitor.Models;
using Monitor.Views;
using Monitor.Views.Settings;
using uWidgets.Core.Models.Attributes;

[assembly: AssemblyCompany("creewick")]
[assembly: AssemblyVersion("1.0.0")]

[assembly: WidgetInfo(typeof(SingleMetric), typeof(SingleMetricModel), typeof(SingleMetricSettings), "Monitor_SingleMetric_Title", "Monitor_SingleMetric_Subtitle")]
[assembly: Locale(typeof(Locale), "Monitor")]