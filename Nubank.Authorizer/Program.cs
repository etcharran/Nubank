using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nubank.Authorizer.Helpers;
using Nubank.Contract;
using Nubank.Domain.Logic;
using Nubank.Domain.Operations;
using Nubank.Persistence.Repositories;

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
                services.AddTransient<ILogger, ConsoleLogger>();
                #region IOperations
                // This factory generates the accountCreation and the Transaction as IOperations
                services.AddSingleton<IOperationFactory, OperationFactory>();
                // For each received accountoperation, it generates a new operation
                services.AddTransient<IOperation<Account>, AccountOperation>();
                // For each received transactionoperation, it generates a new operation
                services.AddTransient<IOperation<Transaction>, TransactionOperation>();
                services.AddScoped<IOperationLogic, OperationLogic>();
                #endregion

                #region Persistence
                // Always in memory for the whole execution of the program
                services.AddSingleton<IAccountRepository, SingleAccountRepository>();
                services.AddSingleton<ITransactionRepository, TransactionRepository>();
                #endregion
                
                services.Configure<ConsoleLifetimeOptions>(options => options.SuppressStatusMessages = true);
                services.AddHostedService<ConsoleApplication>();
            });

    }
}
