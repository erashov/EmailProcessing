using EmailProcessing.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace EmailProcessing.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            Console.WriteLine(configuration.GetConnectionString("Storage"));

            var serviceProvider = new ServiceCollection()
                       .AddLogging()
                       .AddSingleton<IEmailService, EmailService>()
                          .BuildServiceProvider();

            //configure console logging
            serviceProvider
                .GetService<ILoggerFactory>()
                .AddConsole(LogLevel.Debug);

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogDebug("Starting application");

            //do the actual work here
            var bar = serviceProvider.GetService<IEmailService>();
            var cc = bar.PaerserEmailAsync("", "", "", "");
            System.Console.WriteLine($"Count:{cc.Result}");
            System.Console.ReadLine();
            logger.LogDebug("All done!");
        }
    }
}
