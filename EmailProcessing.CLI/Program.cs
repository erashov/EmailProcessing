using EmailProcessing.DAL;
using EmailProcessing.DAL.Entities;
using EmailProcessing.Repository.Abstract;
using EmailProcessing.Repository.Concrete;
using EmailProcessing.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
     
            var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
            var serviceProvider = new ServiceCollection()
                .AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DBConnection"), sql => sql.MigrationsAssembly(migrationsAssembly)))
                       .AddLogging()
                       .AddSingleton<IEmailService, EmailService>()
                       .AddSingleton<ISoapService, SoapService>()
                       .AddTransient<IBaseRepository<Setting>, EmailProcessingRepository>()
                       .BuildServiceProvider();


  
            //do the actual work here
            var emailService = serviceProvider.GetService<IEmailService>();
            var soapService = serviceProvider.GetService<ISoapService>();
            var settings = serviceProvider.GetService<IBaseRepository<Setting>>();
            foreach(var s in settings.Find()) {

                var resultParsing = emailService.PaerserEmailAsync(s, soapService);
              
            }
            
            
            Console.ReadLine();
     
        }
        private static void ConfigureServices(IServiceCollection serviceCollection, string connection)
        {
            serviceCollection.AddLogging()
                       .AddSingleton<IEmailService, EmailService>()
                          .BuildServiceProvider();


        }
    }
}
