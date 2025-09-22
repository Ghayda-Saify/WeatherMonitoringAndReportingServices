using WeatherMonitoringAndReportingService.Bots;
using WeatherMonitoringAndReportingService.InputFiles;

namespace WeatherMonitoringAndReportingService.Bots;

public class RainBot : IObserver
{
    private readonly BotConfiguration _rainBotConfiguration;
    private readonly IObservable _weatherService;

    public RainBot(ConfigurationReader? configurationReader,IObservable weatherService)
    {
        _rainBotConfiguration = configurationReader.RainBotConfig;
        _weatherService = weatherService;
    }
    
    /// <summary>
    /// Here we Update the weather data whenever Update method is called
    /// -> then we call the display method if the condition is true
    /// </summary>
    public void Update()
    {
        var weatherData = _weatherService.WeatherData;
        // If the Humidity is more than the Threshold (from the config file) then Display message.
        if (weatherData.Humidity > _rainBotConfiguration.Threshold)
        {
            Display();
        }
    }
    
    /// <summary>
    /// Display the Bot's message that has been read from config. file
    /// </summary>
    public void Display()
    {
        Console.WriteLine("RainBot activated!");
        Console.WriteLine($"RainBot: {_rainBotConfiguration.Message}");
    }

}