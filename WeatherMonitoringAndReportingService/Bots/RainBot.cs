using WeatherMonitoringAndReportingService.Bots;
using WeatherMonitoringAndReportingService.InputFiles;

namespace WeatherMonitoringAndReportingService.Bots;

public class RainBot : IObserver
{
    private readonly BotConfiguration _rainBotConfiguration;
    private WeatherData _weatherData; 
    private readonly IObservable _weatherService;
    

    public RainBot(ConfigurationReader configurationReader,IObservable weatherService)
    {
        this._rainBotConfiguration = configurationReader.RainBotConfig;
        this._weatherService = weatherService;
    }
    /// <summary>
    /// Here we Update the weather data whenever Update method is called
    /// -> then we call the display method if the condition is true
    /// </summary>
    public void Update()
    {
        this._weatherData = _weatherService.WeatherData;
        // If the Humidity is more than the Threshold (from the config file) then Display message.
        if (_weatherData.Humidity > _rainBotConfiguration.Threshold)
        {
            this.Display();
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