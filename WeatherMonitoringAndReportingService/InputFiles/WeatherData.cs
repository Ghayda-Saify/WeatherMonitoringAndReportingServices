namespace WeatherMonitoringAndReportingService.InputFiles;

public record struct WeatherData(String Location, decimal Temperature, decimal Humidity);
