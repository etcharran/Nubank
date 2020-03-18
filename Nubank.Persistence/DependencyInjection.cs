
using Microsoft.Extensions.DependencyInjection;
using Nubank.Persistence.Repositories;

namespace Nubank.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection BuildInjection(IServiceCollection services)
        {
            #region Persistence
            // Always in memory for the whole execution of the program
            services.AddSingleton<IAccountRepository, SingleAccountRepository>();
            services.AddSingleton<ITransactionRepository, TransactionRepository>();
            #endregion

            return services;
        }
    }
}