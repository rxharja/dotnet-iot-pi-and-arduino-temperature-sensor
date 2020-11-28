using System;

namespace dotnetIOT
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Starting");
            Port serialPort = new Port("/dev/ttyACM0", 9600);
            serialPort.ReceiveData();
            Console.WriteLine("Ending Stream");
        }
    }
}
