using System.Text;
using WeatherMonitoringAndReportingService.Bots;
using WeatherMonitoringAndReportingService.InputFiles;
using WeatherMonitoringAndReportingService.Observable;


public class Program
{
    private static IFileReader _fileReader;
    private static string? _scontentFilePath;
    private static WeatherService _weatherService;
    private static RainBot _rainBot;
    private static SnowBot _snowBot;
    private static SunBot _sunBot;
    private static ConfigurationReader _configurationReader;
    private static StringBuilder _welcomeMessage;

    public Program()
    {
        _welcomeMessage = new StringBuilder();
        _welcomeMessage.Append("======================================");
        _welcomeMessage.Append("-----------------WELCOME TO WEATHER STATION-----------------");
        _welcomeMessage.Append("Please Enter your Data in format of JSON/XML : ");
    }
    
    public static async Task Main(string[] args)
    {
        // Starting the Program
        Console.WriteLine(_welcomeMessage);
        // Read Configuration File
        _configurationReader = new ConfigurationReader();
        try
        {
            await _configurationReader.ReadConfigurationFileAsync(
                "C:\\Users\\ASUS\\RiderProjects\\WeatherMonitoringAndReportingService" +
                "\\WeatherMonitoringAndReportingService\\ConfigurationFile.json");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        // Start the App
        Console.WriteLine("======================================");
        Console.WriteLine("-----------------WELCOME TO WEATHER STATION-----------------");
        Console.WriteLine("Please Enter your Data in format of JSON/XML : ");
        
        // Read the path of the DataFile
        _scontentFilePath = Console.ReadLine();
        string extension = Path.GetExtension(_scontentFilePath) ?? new string("");
        switch (extension.ToLower())
        {
            case ".json":
                _fileReader = new JsonWeatherReader();
                break;
            case ".xml":
                _fileReader = new XmlWeatherReader();
                break;
        }

        await _fileReader.ReadWeatherDataFromFileAsync(_scontentFilePath);

        // Now we can start with WeatherService :
        _weatherService = new WeatherService(_fileReader);

        _rainBot = new RainBot(_configurationReader, _weatherService);
        _snowBot = new SnowBot(_configurationReader, _weatherService);
        _sunBot = new SunBot(_configurationReader, _weatherService);

        _weatherService.Add(_rainBot);
        _weatherService.Add(_snowBot);
        _weatherService.Add(_sunBot);
        _weatherService.Notify();
    }
}


