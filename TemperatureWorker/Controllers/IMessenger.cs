namespace TemperatureWorker.Controllers
{
    public interface IMessenger
    {
        void ProcessData(string data);
    }
}