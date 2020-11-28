using System;
using System.Collections.Generic;
using System.Text;

namespace dotnetIOT
{
    public class IoMessage
    {
        public string value { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string created_at { get; set; }
        public int epoch { get; set; }
    }
}
