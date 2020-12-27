using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Systemd;
using Microsoft.Extensions.Logging;
using System;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using TemperatureWorker.Controllers;

namespace TemperatureWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private readonly IMessenger _messenger;

        private readonly ISerialPortManager _serialPort;

        public Worker(ILogger<Worker> logger, 
            IMessenger messenger, 
            ISerialPortManager serialport)
        {
            _logger = logger;
            
            _messenger = messenger;
            
            _serialPort = serialport;
            
            _serialPort.Start();

            if (SystemdHelpers.IsSystemdService())
            {
                _logger.LogInformation("Running as linux daemon");
            }
            else
            {
                _logger.LogInformation("Not running as Linux daemon");
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            while (!stoppingToken.IsCancellationRequested)
            {

                _logger.LogInformation($"Checking Message Bus at {DateTimeOffset.Now}");

                using var subscriber = _serialPort.MessageBus.Subscribe(
                    message => _messenger.ProcessData(message));

                await Task.Delay(5000, stoppingToken);

            }

        }
    }
}
