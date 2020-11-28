using System;
using System.Collections.Generic;
using System.Text;

namespace dotnetIOT
{
    class DataPoint
    {
        public float c { get; set; }
        public float f { get; set; }
        public string timestamp
        {
            get
            {
                return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
    }
}
