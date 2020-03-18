using Microsoft.Extensions.DependencyInjection;
using Nubank.Contract;
using Nubank.Domain.Operations;
using Nubank.Domain.Validation;

namespace Nubank.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection BuildInjection(IServiceCollection services)
        {

            #region IOperations
            // This factory generates the accountCreation and the Transaction as IOperations
            services.AddTransient<IOperationLogic, OperationLogic>();
            // For each received accountoperation, it generates a new operation
            services.AddTransient<IOperation<Account>, AccountOperation>();
            // For each received transactionoperation, it generates a new operation
            services.AddTransient<IOperation<Transaction>, TransactionOperation>();
            #endregion

            #region IValidations
            services.AddTransient<DoubledTransactionValidation>();
            services.AddTransient<HighFrequencyValidation>();
            services.AddTransient<InactiveCardValidation>();
            services.AddTransient<InitializedAccountValidation>();
            services.AddTransient<InsufficientLimitValidation>();
            services.AddTransient<ValidationResponse>();
            #endregion

            Nubank.Persistence.DependencyInjection.BuildInjection(services);

            return services;
        }
    }
}