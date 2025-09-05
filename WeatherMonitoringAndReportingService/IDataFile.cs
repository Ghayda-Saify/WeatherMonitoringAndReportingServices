namespace WeatherMonitoringAndReportingService;

public interface IDataFile
{
    public List<string> ReadFile(String path);
}