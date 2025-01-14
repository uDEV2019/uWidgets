using Avalonia.Controls;
using Avalonia.Media;

namespace Weather.ViewModels;

public record DailyForecastViewModel(string DayOfWeek, StreamGeometry Icon, string Min, string Max, List<GridLength> Graph);