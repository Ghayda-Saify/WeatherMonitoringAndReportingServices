using System.Text;
using WeatherMonitoringAndReportingService.Bots;
using WeatherMonitoringAndReportingService.InputFiles;
using WeatherMonitoringAndReportingService.Observable;

namespace WeatherMonitoringAndReportingService;

public abstract class Program
{
    private static ConfigurationReader? _configurationReader;

    protected Program()
    {
        _configurationReader = new ConfigurationReader();
    }

    private static StringBuilder GetMessage()
    {
        var welcomeMessage = new StringBuilder();
        welcomeMessage.Append("======================================");
        welcomeMessage.Append("-----------------WELCOME TO WEATHER STATION-----------------");
        welcomeMessage.Append("Please Enter your Data in format of JSON/XML : ");
        return welcomeMessage;
    }
    public static async Task Main(string[] args)
    {
        // Starting the Program
        Console.WriteLine(GetMessage());
        
        // Select file reader based on weather the file is json / xml 
        var fileReader = await SelectTheAppropriateInputFileReader();

        // Now we can start with WeatherService :
        var weatherService = new WeatherService(fileReader);

        var rainBot = new RainBot(_configurationReader, weatherService);
        var snowBot = new SnowBot(_configurationReader, weatherService);
        var sunBot = new SunBot(_configurationReader, weatherService);

        weatherService.Add(rainBot);
        weatherService.Add(snowBot);
        weatherService.Add(sunBot);
        weatherService.Notify();
    }

    private static async Task<IFileReader> SelectTheAppropriateInputFileReader()
    {
        try
        {
            if (_configurationReader != null)
                await _configurationReader.ReadConfigurationFileAsync(
                    "C:\\Users\\ASUS\\RiderProjects\\WeatherMonitoringAndReportingService" +
                    "\\WeatherMonitoringAndReportingService\\ConfigurationFile.json");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        
        // Read the path of the DataFile
        var contentFilePath = Console.ReadLine() ?? " ";
        var extension = Path.GetExtension(contentFilePath);
        IFileReader fileReader = extension.ToLower() switch
        {
            ".json" => new JsonWeatherReader(),
            ".xml" => new XmlWeatherReader(),
            _ => new JsonWeatherReader()
        };

        await fileReader.ReadWeatherDataFromFileAsync(contentFilePath);
        
        return fileReader;
    }
}


