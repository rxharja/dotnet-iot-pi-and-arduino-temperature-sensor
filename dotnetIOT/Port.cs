using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using Newtonsoft.Json;

namespace dotnetIOT
{
    class Port
    {
        public string PortName { get; set; }
        public int BaudRate { get; set; }
        private SerialPort _Port { get; set; }

        private Message Messager = new Message();

        private string Buffer { get; set; }

        public Port(string port, int baudrate)
        {
            this.PortName = port;
            this.BaudRate = baudrate;
        }

        public void ReceiveData()
        {
            _Port = new SerialPort(this.PortName, this.BaudRate, Parity.None);
            //_Port.DataReceived += new SerialDataReceivedEventHandler(DataReceived);
            _Port.Open();
            //Console.ReadLine();
            while (true)
            {
                try
                {
                    this.Buffer = _Port.ReadLine().Trim();
                    DataPoint datapoint = JsonConvert.DeserializeObject<DataPoint>(this.Buffer);
                    IoMessage messageC = new IoMessage
                    {
                        value = datapoint.c.ToString(),
                        created_at = datapoint.timestamp
                    };
                    IoMessage messageF = new IoMessage
                    {
                        value = datapoint.f.ToString(),
                        created_at = datapoint.timestamp
                    };
                    Messager.Send(JsonConvert.SerializeObject(messageC), "c");
                    Messager.Send(JsonConvert.SerializeObject(messageF), "f");
                }
                catch (JsonReaderException)
                {
                    Console.Write("Improperly formatted JSON string from buffer, trying again");
                }

                this.Buffer = string.Empty;

            }
        }

        //private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{
        //    this.Buffer += _Port.ReadExisting();
        //    if (this.Buffer.Contains("\n"))
        //    {
        //        //Console.WriteLine(this.Buffer);
        //        try
        //        {
        //            DataPoint datapoint = JsonConvert.DeserializeObject<DataPoint>(this.Buffer.Trim());
        //            IoMessage messageC = new IoMessage
        //            {
        //                value = datapoint.c.ToString(),
        //                created_at = datapoint.timestamp
        //            };
        //            IoMessage messageF = new IoMessage
        //            {
        //                value = datapoint.f.ToString(),
        //                created_at = datapoint.timestamp
        //            };
        //            Messager.Send(JsonConvert.SerializeObject(messageC), "c");
        //            Messager.Send(JsonConvert.SerializeObject(messageF), "f");
        //        }
        //        catch (JsonReaderException)
        //        {
        //            Console.Write("Improperly formatted JSON string from buffer, trying again");
        //        }
        //        this.Buffer = string.Empty;
        //    }
        //}
    }
}
