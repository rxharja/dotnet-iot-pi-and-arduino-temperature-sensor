using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TemperatureWorker.Controllers;
using TemperatureWorker.Services;
using TemperatureWorker.Configuration;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace TemperatureWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSystemd()
                .ConfigureAppConfiguration((context, configuration) =>
                {
                    configuration.SetBasePath(Directory.GetCurrentDirectory());
                    configuration.AddJsonFile("appsettings.json", optional: false);
                    configuration.AddEnvironmentVariables();
                    configuration.Build();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions();
                    services.Configure<PortConfiguration>(hostContext.Configuration.GetSection("PortConfig"));
                    services.AddSingleton<IMessage, Message>();
                    services.AddSingleton<ISerialPortManager, SerialPortManager>();
                    services.AddSingleton<IMessenger, Messenger>();
                    services.AddHostedService<Worker>();
                });
    }
}
