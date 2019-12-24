using Microsoft.Extensions.DependencyInjection;
using Nubank.Contract;
using Nubank.Tools.Exceptions;
using System;

namespace Nubank.Domain.Operations
{
    public class OperationFactory : IOperationFactory
    {
        private readonly IServiceProvider serviceProvider;
        public OperationFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Retrieve an operation matching the data type and build it
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IOperation CreateOperation(IData data)
        {
            switch (data.Name)
            {
                case Account.name:
                    return serviceProvider.GetService<IOperation<Account>>().Build(data as Account) as IOperation;
                case Transaction.name:
                    return serviceProvider.GetService<IOperation<Transaction>>().Build(data as Transaction) as IOperation;
                default:
                    throw new UnSupportedOperationException();
            }
        }
    }
}
