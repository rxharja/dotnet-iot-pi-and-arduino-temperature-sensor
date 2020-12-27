using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using Newtonsoft.Json;
using TemperatureWorker.Services;
using TemperatureWorker.Data;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace TemperatureWorker.Controllers
{
    class Messenger : IMessenger
    {
        private readonly IMessage _message;

        private readonly ILogger<Messenger> _logger;

        private IOMessage messageC;

        private IOMessage messageF;

        private DataPoint datapoint;


        public Messenger(IMessage message, ILogger<Messenger> logger)
        {
            this._message = message;
            _logger = logger;
        }

        public void ProcessData(string data)
        {
            //_logger.LogInformation($"data coming in: {data}");
            try
            {
                datapoint = JsonConvert.DeserializeObject<DataPoint>(data);
            }
            catch (JsonReaderException)
            {
                _logger.LogWarning("Improperly formatted JSON string from buffer, trying again");
            }

            messageC.value = datapoint.C.ToString();
            messageC.created_at = datapoint.Timestamp;

            messageF.value = datapoint.F.ToString();
            messageF.created_at = datapoint.Timestamp;

            _message.Send(JsonConvert.SerializeObject(messageC), "c");
            _message.Send(JsonConvert.SerializeObject(messageF), "f");
        }
    }
}
