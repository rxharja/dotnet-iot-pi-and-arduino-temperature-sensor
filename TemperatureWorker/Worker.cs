using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TemperatureWorker.Configuration;
using TemperatureWorker.Controllers;

namespace TemperatureWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private readonly IMessenger _messenger;

        private readonly SerialPort serialPort;

        private readonly IOptions<PortConfiguration> _portconfig;
        private string Buffer { get; set; }


        public Worker(ILogger<Worker> logger, 
            IMessenger messenger, 
            SerialPort serialport, 
            IOptions<PortConfiguration> options)
        {
            _logger = logger;
            _messenger = messenger;
            serialPort = serialport;
            _portconfig = options;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            serialPort.PortName = _portconfig.Value.PortName;
            serialPort.BaudRate = _portconfig.Value.BaudRate;
            serialPort.Parity = Parity.None;

            serialPort.Open();
            
            while (!stoppingToken.IsCancellationRequested)
            {
                Buffer = serialPort.ReadLine().Trim();
                _logger.LogInformation("Accessing port at: {time}", DateTimeOffset.Now);
                _messenger.ProcessData(Buffer);
                await Task.Delay(100, stoppingToken);
                Buffer = String.Empty;
            }

            serialPort.Close();
        }
    }
}
