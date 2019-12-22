using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nubank.Authorizer.Helpers;
using Nubank.Contract;
using Nubank.Domain.Logic;
using Nubank.Domain.Operations;
using Nubank.Persistence;
using Nubank.Persistence.Repositories;

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
                services.AddScoped<IOperation<Account>, AccountOperation>();
                services.AddScoped<IOperation<Transaction>, TransactionOperation>();
                services.AddScoped<IOperationLogic, OperationLogic>();
                #endregion

                #region Persistence
                services.AddSingleton<IRepository<Account>, AccountRepository>();
                #endregion

                services.AddHostedService<ConsoleApplication>();
            });

    }
}
