namespace TemperatureWorker.Services
{
    interface IMessage
    {
        void Send(string ioMessageText, string feed);
    }
}