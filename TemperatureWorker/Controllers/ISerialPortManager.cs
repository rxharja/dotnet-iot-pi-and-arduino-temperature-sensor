using System;

namespace TemperatureWorker.Controllers
{
    public interface ISerialPortManager
    {
        IObservable<string> MessageBus { get; }
        string Name { get; }

        void Start();
    }
}