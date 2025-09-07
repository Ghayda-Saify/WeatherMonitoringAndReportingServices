using System.Xml.Serialization;

namespace WeatherMonitoringAndReportingService.InputFiles;

public class XmlWeatherReader : IFileReader
{
    public WeatherData WeatherData { get; set; }

    /// <summary>
    /// Read Weather Data From XML File Async. and return it as a WeatherData record type
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns> WeatherData type </returns>
    public async Task ReadWeatherDataFromFileAsync(String filePath)
    {
        try
        {
            var serializer = new XmlSerializer(typeof(WeatherData));
            await using var fileStream = new FileStream(filePath, FileMode.Open);
            var result = (WeatherData?)serializer.Deserialize(fileStream);
            WeatherData = result ?? new WeatherData();
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Error: The file was not found at '{filePath}'.");
            WeatherData = new WeatherData();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error: Failed to parse XML file. Details: {ex.Message}");
            WeatherData = new WeatherData();
        }
    }
}