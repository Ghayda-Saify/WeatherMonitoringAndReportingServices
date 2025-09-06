using WeatherMonitoringAndReportingService.Bots;
using WeatherMonitoringAndReportingService.InputFiles;

namespace WeatherMonitoringAndReportingService.Bots;

public class SunBot: IObserver
{
    private readonly BotConfiguration _sunBotConfiguration;
    private WeatherData _weatherData; 
    private readonly IObservable _weatherService;

    public SunBot(ConfigurationReader configurationReader, IObservable weatherService)
    {
        this._sunBotConfiguration = configurationReader.SunBotConfig;
        this._weatherService = weatherService;
    }
    public void Update()
    {
        this._weatherData = _weatherService.WeatherData;
        // If the Temperature is more than the Threshold (from the config file) then Display message.
        if (_weatherData.Temperature > _sunBotConfiguration.Threshold)
        {
            this.Display();
        }
    }

    public void Display()
    {
        Console.WriteLine("SunBot activated!");
        Console.WriteLine($"SunBot: {_sunBotConfiguration.Message}");
    }
}