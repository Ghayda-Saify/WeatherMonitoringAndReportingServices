using WeatherMonitoringAndReportingService.InputFiles;

namespace WeatherMonitoringAndReportingService;

public interface IObservable
{
    public WeatherData WeatherData { get; set; }
    public void Add(IObserver observer);
    public void Remove(IObserver observer);
    public void Notify();
}