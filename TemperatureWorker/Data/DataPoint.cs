using System;
using System.Collections.Generic;
using System.Text;

namespace TemperatureWorker.Data
{
    public struct DataPoint
    {
        public float C { get; set; }
        public float F { get; set; }
        public string Timestamp
        {
            get
            {
                return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
    }
}
