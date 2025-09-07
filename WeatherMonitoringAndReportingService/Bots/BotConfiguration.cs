namespace WeatherMonitoringAndReportingService.Bots;

public record struct BotConfiguration(bool Enabled, decimal Threshold, string Message);
