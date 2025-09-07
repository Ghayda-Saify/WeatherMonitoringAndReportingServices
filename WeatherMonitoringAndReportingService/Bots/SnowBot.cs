using WeatherMonitoringAndReportingService.Bots;
using WeatherMonitoringAndReportingService.InputFiles;

namespace WeatherMonitoringAndReportingService.Bots;

public class SnowBot: IObserver
{
    private readonly BotConfiguration _snowBotConfiguration;
    private WeatherData _weatherData; 
    private readonly IObservable _weatherService;
    
    public SnowBot(ConfigurationReader configurationReader , IObservable weatherService)
    {
        this._snowBotConfiguration = configurationReader.SnowBotConfig;
        this._weatherService = weatherService;
    }
    /// <summary>
    /// Here we Update the weather data whenever Update method is called
    /// -> then we call the display method if the condition is true
    /// </summary>
    public void Update()
    {
        this._weatherData = _weatherService.WeatherData;
        // If the Temperature is less than the Threshold (from the config file) then Display message.
        if (_weatherData.Temperature < _snowBotConfiguration.Threshold)
        {
            this.Display();
        }
    }
    /// <summary>
    /// Display the Bot's message that has been read from config. file
    /// </summary>
    public void Display()
    {
        Console.WriteLine("SnowBot activated!");
        Console.WriteLine($"SnowBot: {_snowBotConfiguration.Message}");
    }
}