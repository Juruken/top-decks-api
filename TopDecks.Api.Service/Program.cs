using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace TopDecks.Api.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConfigureLogging();

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }

        private static void ConfigureLogging()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("settings.json")
                .Build();

            var assemblyName = Assembly.GetEntryAssembly().GetName();
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", assemblyName.Name)
                .Enrich.WithProperty("Version", assemblyName.Version)
                .Enrich.WithMachineName()
                .Enrich.WithProcessId()
                .Enrich.WithThreadId()
                .MinimumLevel.Debug()
                .WriteTo.Seq(config["seqApplicationUrl"], apiKey: config["seqApplicationKey"])
                .WriteTo.ColoredConsole()
                .CreateLogger();
        }
    }
}
