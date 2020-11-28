using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;


namespace dotnetIOT
{
    class Message
    {
        private string ADAFRUIT_IO_USERNAME = "rxharja";
        private string ADAFRUIT_IO_KEY = "aio_eVPT97R8pw5bbK3YkI6zpUmoepiI";
        private string ADAFRUIT_IO_FEED = "temperature";

        public void Send(string ioMessageText, string feed)
        {
            string postDestination = $"https://io.adafruit.com/api/v2/{this.ADAFRUIT_IO_USERNAME}/feeds/{this.ADAFRUIT_IO_FEED}{feed}/data";
            StringContent stringContent = new StringContent(ioMessageText, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            client.DefaultRequestHeaders.TryAddWithoutValidation("X-AIO-Key", this.ADAFRUIT_IO_KEY);
            var response = client.PostAsync(postDestination, stringContent).Result;

            Console.WriteLine($"Adafruit IO responds to {ioMessageText}: {response.IsSuccessStatusCode}");
        }

    }
}
