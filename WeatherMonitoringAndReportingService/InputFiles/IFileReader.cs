namespace WeatherMonitoringAndReportingService.InputFiles;

public interface IFileReader
{
    public WeatherData WeatherData { get; set; }

    public Task ReadWeatherDataFromFileAsync(String filePath);

}