using System.Text.Json;
using System.Text.Json.Nodes;
using WeatherMonitoringAndReportingService.Bots;

namespace WeatherMonitoringAndReportingService.InputFiles;

public class ConfigurationReader
{

    public BotConfiguration RainBotConfig { get; set; }
    public BotConfiguration SunBotConfig { get; set; }
    public BotConfiguration SnowBotConfig { get; set; }

    /// <summary>
    /// Read Configuration File Async (JSON File)
    /// </summary>
    /// <param name="filePath"></param>
    public async Task ReadConfigurationFileAsync(string filePath)
    {
        try
        {
            // Read the entier file content as a string.
            string jsonContent = await File.ReadAllTextAsync(filePath);
            // Parse Into JsonNode obj (the ! means we expect this to not be a null).
            JsonNode rootNode = JsonNode.Parse(jsonContent)!;  
            
            // Access each prop by it's key and deserialize it by it's own.
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            
            // Now we will Assign each Config. data to it's container.
            RainBotConfig = rootNode["RainBot"]!.Deserialize<BotConfiguration>(options);
            SunBotConfig = rootNode["SunBot"]!.Deserialize<BotConfiguration>(options);
            SnowBotConfig = rootNode["SnowBot"]!.Deserialize<BotConfiguration>(options);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        
    }
}