using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nubank.Authorizer.Helpers;

namespace Nubank.Authorizer
{
    public class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<ILogger, ConsoleLogger>();
                services.Configure<ConsoleLifetimeOptions>(options => options.SuppressStatusMessages = true);

                Nubank.Domain.DependencyInjection.BuildInjection(services);

                services.AddHostedService<ConsoleApplication>();
            });

    }
}
