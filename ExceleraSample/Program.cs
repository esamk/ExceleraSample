using ExceleraSample.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SampleDB.Contexts;
using SampleDB.Interfaces;
using SampleDB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExceleraSample
{
    static class Program
    {
        public static IConfiguration Configuration { get; private set; }
        
        public static IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Configuration 
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();

            // Host
            var host = CreateHostBuilder().Build();
            ServiceProvider = host.Services;

            //GetRequiredService vaatii: using Microsoft.Extensions.DependencyInjection 
            Application.Run(ServiceProvider.GetRequiredService<Form1>());
        }

        

        
        // IHostBuilder vaatii Microsoft.Extensions.Hosting.Abstractions 
        // nuget paketin installoinnin. 
        static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => {

                    // haetaan connection string appsettings json:sta
                    services.AddDbContext<SampleContext>(options => {
                        options.UseSqlServer(Configuration.GetConnectionString("SampleContext"));
                    });

                    // DI injektoi IContextin repositoreihin.
                    // Tarvitaan, jotta voidaan tehd� SaveAsync suoraan repositoryn kautta,
                    // eik� formeille tarvita repositoryn lis�ksi viittauksia IContextiin
                    // tai SampleContextiin.
                    // Ks. RepositoryBase.SaveAsync()
                    services.AddTransient<IContext, SampleContext>();

                    // repositoryt 
                    
                    // Ks. ExceleraSample.Extensions.RepositoryFinder
                    services.AddRepositories(typeof(SampleContext).Assembly);
                    
                    // T�m� olis toinen tapa lis�t� repositoryt DI:en.
                    //services.AddTransient<IProductRepository, ProductRepository>();
                    //services.AddTransient<IOrderRepository, OrderRepository>();
                    //services.AddTransient<IOrderLineRepository, OrderLineRepository>();

                    // Formit
                    services.AddTransient<Form1>();
                });
        }
    }
}
