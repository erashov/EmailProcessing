using EmailProcessing.DAL;
using EmailProcessing.DAL.Entities;
using EmailProcessing.Repository.Abstract;
using EmailProcessing.Repository.Concrete;
using EmailProcessing.Service;
using Microsoft.EntityFrameworkCore;
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
          //  var serviceCollection = new ServiceCollection();
           // ConfigureServices(serviceCollection, configuration.GetConnectionString("DBConnection"));

             Console.WriteLine(configuration.GetConnectionString("DBConnection"));

            var serviceProvider = new ServiceCollection().AddDbContextPool<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DBConnection")))
                       .AddLogging()
                       .AddSingleton<IEmailService, EmailService>()  .AddTransient<IBaseRepository<Setting>, EmailProcessingRepository>()
                          .BuildServiceProvider();

           

            serviceProvider
                .GetService<ILoggerFactory>()
                .AddConsole(LogLevel.Debug);

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogDebug("Starting application");

            //do the actual work here
            var bar = serviceProvider.GetService<IEmailService>();
            var settings = serviceProvider.GetService<IBaseRepository<Setting>>();
            var c=settings.Find();
            var cc = bar.PaerserEmailAsync("", "", "", "");
            System.Console.WriteLine($"Count:{cc.Result}");
            System.Console.ReadLine();
            logger.LogDebug("All done!");
        }
        private static void ConfigureServices(IServiceCollection serviceCollection, string connection)
        {
            serviceCollection.AddLogging()
                       .AddSingleton<IEmailService, EmailService>()
                          .BuildServiceProvider();

            //configure console logging
            //serviceCollection
            //    .GetService<ILoggerFactory>()
            //    .AddConsole(LogLevel.Debug);
            //serviceCollection.AddDbContextPool<AppDbContext>(options =>
            //             options.UseSqlServer(connection));
            //serviceCollection.AddTransient<ITestService, TestService>();

            // add app
           // serviceCollection.AddTransient<App>();
        }
    }
}
