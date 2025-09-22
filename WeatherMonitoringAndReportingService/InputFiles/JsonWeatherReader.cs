using System.Text.Json;

namespace WeatherMonitoringAndReportingService.InputFiles;


public class JsonWeatherReader: IFileReader
{
    public WeatherData WeatherData { get; set; }
    /// <summary>
    /// Read Weather Data From JSON File Async. and return it as a WeatherData record type.
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns> List of WeatherData type </returns>
    public async Task ReadWeatherDataFromFileAsync(String filePath)
    {
        try
        {
            // Read the entier file content as a string.
            string jsonContent = await File.ReadAllTextAsync(filePath);
            // Deserialize the JSON string into a List of WeatherData objects.
            WeatherData = JsonSerializer.Deserialize<WeatherData>(jsonContent);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Error: The file was not found at '{filePath}'.");
            WeatherData = new WeatherData();
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error: The file '{filePath}' contains invalid JSON. Details: {ex.Message}");
            WeatherData = new WeatherData();
        }
    }

  

}