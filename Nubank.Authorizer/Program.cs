using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nubank.Authorizer.Helpers;
using Nubank.Authorizer.Logic;
using Nubank.Authorizer.Operations;

namespace Nubank.Authorizer
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => 
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddTransient<ILogger, ConsoleLogger>();
                #region IOperations
                // This factory generates the accountCreation and the Transaction as IOperations
                services.AddSingleton<IOperationFactory, OperationFactory>();
                services.AddTransient<IOperation, AccountCreation>();
                services.AddTransient<IOperation, Transaction>();
                #endregion

                services.AddTransient<IOperationLogic, OperationLogic>();
                services.AddHostedService<ConsoleApplication>();
            });

    }
}
