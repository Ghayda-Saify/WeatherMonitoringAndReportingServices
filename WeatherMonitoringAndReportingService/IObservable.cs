namespace WeatherMonitoringAndReportingService;

public interface IObservable : IObserverOfBots
{
    public void Add(IObserverOfBots observerOfBots);
    public void Remove(IObserverOfBots observerOfBots);
    public void Notify();
}