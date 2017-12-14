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
using System.Reflection;

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
            var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
            var serviceProvider = new ServiceCollection().AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DBConnection"), sql => sql.MigrationsAssembly(migrationsAssembly)))
                       .AddLogging()
                       .AddSingleton<IEmailService, EmailService>().AddTransient<IBaseRepository<Setting>, EmailProcessingRepository>()
                          .BuildServiceProvider();


  
            //do the actual work here
            var bar = serviceProvider.GetService<IEmailService>();
            var settings = serviceProvider.GetService<IBaseRepository<Setting>>();
            foreach(var s in settings.Find()) {

                var cc = bar.PaerserEmailAsync(s);
                Console.WriteLine($"Count:{cc.Result}");
            }
            
            
            Console.ReadLine();
     
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
