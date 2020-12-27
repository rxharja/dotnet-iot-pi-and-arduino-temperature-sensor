using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO.Ports;
using System.Reactive.Subjects;
using System.Threading;
using TemperatureWorker.Configuration;

namespace TemperatureWorker.Controllers
{
    public class SerialPortManager : ISerialPortManager
    {
        private readonly ILogger<Worker> _logger;

        private readonly IOptions<PortConfiguration> _portconfig;

        Subject<string> messageBus = new Subject<string>();
        public IObservable<string> MessageBus => messageBus;

        public string Name { get; private set; }

        private CancellationTokenSource cts = new CancellationTokenSource();
        private SerialPort serialPort;

        public SerialPortManager(IOptions<PortConfiguration> options, ILogger<Worker> logger)
        {
            _portconfig = options;
            _logger = logger;
        }

        public void Start()
        {
            ThreadStart ts = new ThreadStart(SerialDeviceThread);
            Thread t = new Thread(ts)
            {
                IsBackground = true,
                Name = Name
            };
            t.Start();
        }

        private void SerialDeviceThread()
        {
            serialPort = new SerialPort(_portconfig.Value.PortName, _portconfig.Value.BaudRate);
            
            serialPort.Open();
            
            while (true)
            {
                string line = serialPort.ReadLine();

                messageBus.OnNext(line);
            }

            serialPort.Close();
            
            serialPort.Dispose();
        }
    }
}
