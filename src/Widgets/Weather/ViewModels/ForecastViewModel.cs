using Avalonia.Controls;
using Avalonia.Media;
using ReactiveUI;
using uWidgets.Services;
using Weather.Models;
using Weather.Models.Forecast;
using Weather.Services;

namespace Weather.ViewModels;

public class ForecastViewModel : ReactiveObject, IDisposable
{
    private readonly ForecastModel model;
    private readonly OpenMeteoWeatherProvider provider;

    public ForecastViewModel(ForecastModel model)
    {
        provider = new OpenMeteoWeatherProvider();
        this.model = model;
        TimerService.Timer1Hour.Subscribe(UpdateForecast);
        UpdateForecast();
    }

    private void UpdateForecast() => _ = UpdateForecastAsync();

    private async Task UpdateForecastAsync()
    {
        var forecast = await provider.GetForecastAsync(model.Latitude, model.Longitude, model.TemperatureUnit);
        if (forecast is null) return;

        var currentHour = DateTime.Now.Hour;

        CurrentTemperature = $"{forecast.Current.Temperature:0}°";
        CurrentIcon = forecast.Hourly.IsDay[currentHour] > 0 ? WeatherIconProvider.GetIcon(forecast.Current.WeatherCode) : WeatherIcon.Night.Value;
        CurrentCondition = GetCondition(forecast.Current.WeatherCode);
        CurrentMinMax = $"{forecast.Daily.Max[0]:0}°  {forecast.Daily.Min[0]:0}°";
        CurrentMin = $"{forecast.Daily.Min[0]:0}";
        CurrentMax = $"{forecast.Daily.Max[0]:0}";
        TemperatureMetric = new(forecast.Daily.Min.Min(), forecast.Daily.Max.Max(), forecast.Current.Temperature, null);
        UVIndex = new(0, 10, forecast.Hourly.UVIndex[currentHour], WeatherIcon.Clear.Value);
        Pressure = new(960, 1050, forecast.Current.Pressure, WeatherIcon.Pressure.Value);
        SunsetSunrise = GetSunsetSunrise(forecast);
        HourlyForecast = Enumerable
            .Range(currentHour, 24)
            .Select(hour => GetHourlyForecast(forecast, hour % 24));
        DailyForecast = Enumerable
            .Range(0, forecast.Daily.Min.Count)
            .Select(day => GetDailyForecast(forecast, day));
    }

    private SunriseSunsetViewModel GetSunsetSunrise(ForecastResponse forecast)
    {
        var sunrise = forecast.Daily.Sunrise[0];
        var sunset = forecast.Daily.Sunset[0];
        var sunriseTomorrow = forecast.Daily.Sunrise[1];
        var now = DateTime.Now;

        if (now < sunrise)
            return new(WeatherIcon.Sunrise.Value, sunrise.ToString("HH:mm"));
        if (now < sunset)
            return new(WeatherIcon.Sunset.Value, sunset.ToString("HH:mm"));
        return new(WeatherIcon.Sunrise.Value, sunriseTomorrow.ToString("HH:mm"));
    }

    private DailyForecastViewModel GetDailyForecast(ForecastResponse forecast, int day)
    {
        var dayOfWeek = (int) forecast.Daily.Time[day].DayOfWeek;
        var dayOfWeekName = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.AbbreviatedDayNames[dayOfWeek];
        var code = forecast.Daily.WeatherCode[day];
        var icon = WeatherIconProvider.GetIcon(code);
        var min = forecast.Daily.Min[day];
        var max = forecast.Daily.Max[day];

        var totalMin = forecast.Daily.Min.Min();
        var totalMax = forecast.Daily.Max.Max();
        
        List<GridLength> graph =
        [
            new GridLength(min - totalMin, GridUnitType.Star),
            new GridLength(max - min, GridUnitType.Star),
            new GridLength(totalMax - max, GridUnitType.Star),
        ];
        
        return new DailyForecastViewModel(dayOfWeekName, icon, $"{min:0}°", $"{max:0}°", graph);
    }

    private HourlyForecastViewModel GetHourlyForecast(ForecastResponse forecast, int hour)
    {
        var time = hour.ToString();
        var code = forecast.Hourly.WeatherCode[hour];
        var icon = forecast.Hourly.IsDay[hour] > 0 ? WeatherIconProvider.GetIcon(code) : WeatherIcon.Night.Value;
        var temperature = forecast.Hourly.Temperature[hour];

        return new HourlyForecastViewModel(time, icon, $"{temperature:0}°");
    }
    
    private string? GetCondition(WeatherCode code) => Locales.Locale.ResourceManager.GetString($"Weather_Code_{(int)code}");

    private StreamGeometry currentIcon = new();
    public StreamGeometry CurrentIcon
    {
        get => currentIcon;
        private set => this.RaiseAndSetIfChanged(ref currentIcon, value);
    }

    private IEnumerable<HourlyForecastViewModel> hourlyForecast = [];
    public IEnumerable<HourlyForecastViewModel> HourlyForecast
    {
        get => hourlyForecast;
        private set => this.RaiseAndSetIfChanged(ref hourlyForecast, value);
    }

    private IEnumerable<DailyForecastViewModel> dailyForecast = [];
    public IEnumerable<DailyForecastViewModel> DailyForecast
    {
        get => dailyForecast;
        private set => this.RaiseAndSetIfChanged(ref dailyForecast, value);
    }
    
    public string CityName => model.Name;

    private string? currentTemperature = "--°";
    public string? CurrentTemperature
    {
        get => currentTemperature;
        private set => this.RaiseAndSetIfChanged(ref currentTemperature, value);
    }
    
    private string? currentCondition = "--------";
    public string? CurrentCondition
    {
        get => currentCondition;
        private set => this.RaiseAndSetIfChanged(ref currentCondition, value);
    }
    
    private string? currentMinMax = "--°  --°";
    public string? CurrentMinMax
    {
        get => currentMinMax;
        private set => this.RaiseAndSetIfChanged(ref currentMinMax, value);
    }
        
    private string? currentMin = "--";
    public string? CurrentMin
    {
        get => currentMin;
        private set => this.RaiseAndSetIfChanged(ref currentMin, value);
    }
    
    private string? currentMax = "--";
    public string? CurrentMax
    {
        get => currentMax;
        private set => this.RaiseAndSetIfChanged(ref currentMax, value);
    }

    private MetricViewModel temperatureMetric = new(0, 1, 0, null);
    public MetricViewModel TemperatureMetric
    {
        get => temperatureMetric;
        private set => this.RaiseAndSetIfChanged(ref temperatureMetric, value);
    }
    
    private MetricViewModel uvIndex = new(0, 10, 0, WeatherIcon.Clear.Value);
    public MetricViewModel UVIndex
    {
        get => uvIndex;
        private set => this.RaiseAndSetIfChanged(ref uvIndex, value);
    }

    private MetricViewModel pressure = new(960, 1050, 960, WeatherIcon.Pressure.Value);
    public MetricViewModel Pressure
    {
        get => pressure;
        private set => this.RaiseAndSetIfChanged(ref pressure, value);
    }
    
    private SunriseSunsetViewModel sunsetSunrise = new(new(), "--:--");
    public SunriseSunsetViewModel SunsetSunrise
    {
        get => sunsetSunrise;
        private set => this.RaiseAndSetIfChanged(ref sunsetSunrise, value);
    }

    public void Dispose()
    {
        TimerService.Timer1Hour.Unsubscribe(UpdateForecast);
        GC.SuppressFinalize(this);
    }
}