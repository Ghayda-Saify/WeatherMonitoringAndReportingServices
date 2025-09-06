using WeatherMonitoringAndReportingService.InputFiles;

namespace WeatherMonitoringAndReportingService.Observable;

public class WeatherService  : IObservable
{
    private List<IObserver> _bots;
    public WeatherData WeatherData { get; set; }

    public WeatherService(IFileReader fileReader)
    {
        _bots = new List<IObserver>();
        try
        {
            WeatherData = fileReader.WeatherData;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"There is a problem in WeatherService Constructor! - {ex.Message}");
        }
    }
    public void Add(IObserver observer)
    {
        this._bots.Add(observer);
    }
    public void Remove(IObserver observer)
    {
        this._bots.Remove(observer);
    }
    public void Notify()
    {
        foreach (var observer in this._bots)
        {
            observer.Update();
        }
    }
    
}