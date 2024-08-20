using Avalonia.Collections;
using Avalonia.Media;

namespace Weather.ViewModels;

public record MetricViewModel(double Min, double Max, double Value, StreamGeometry? Icon)
{
    private const double Length = 21;

    public int? DisplayMin => Icon != null ? null : (int)Math.Round(Min);
    public int? DisplayMax => Icon != null ? null : (int)Math.Round(Max);
    public int? DisplayValue => (int)Math.Round(Value);
    
    public AvaloniaList<double> DashArray =>
    [
        (Math.Clamp(Value, Min, Max) - Min) / (Max - Min) * Length, 
        Length * 2
    ];
}