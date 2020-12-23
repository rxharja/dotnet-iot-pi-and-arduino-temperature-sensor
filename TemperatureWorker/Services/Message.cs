using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;

namespace TemperatureWorker.Services
{
    class Message : IMessage
    {
        private string ADAFRUIT_IO_USERNAME = "rxharja";
        private string ADAFRUIT_IO_KEY = "aio_OoXw38YG7OKTuqquOYHljbh6BS8H";
        private string ADAFRUIT_IO_FEED = "temperature";

        private readonly ILogger<Message> _logger;

        public Message(ILogger<Message> logger)
        {
            _logger = logger;
        }

        public void Send(string ioMessageText, string feed)
        {
            string postDestination = $"https://io.adafruit.com/api/v2/{this.ADAFRUIT_IO_USERNAME}/feeds/{this.ADAFRUIT_IO_FEED}{feed}/data";
            StringContent stringContent = new StringContent(ioMessageText, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            client.DefaultRequestHeaders.TryAddWithoutValidation("X-AIO-Key", this.ADAFRUIT_IO_KEY);
            var response = client.PostAsync(postDestination, stringContent).Result;

            _logger.LogInformation($"Adafruit IO responds to {ioMessageText}: {response.IsSuccessStatusCode}");

            client.Dispose();
        }

    }
}
