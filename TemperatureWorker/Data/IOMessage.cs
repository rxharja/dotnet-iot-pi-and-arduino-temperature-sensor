using System;
using System.Collections.Generic;
using System.Text;

namespace TemperatureWorker.Data
{
    public struct IOMessage
    {
        public string value { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string created_at { get; set; }
        public int epoch { get; set; }
    }
}
