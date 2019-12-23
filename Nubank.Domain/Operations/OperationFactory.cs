using Microsoft.Extensions.DependencyInjection;
using Nubank.Contract;
using Nubank.Tools;
using System;
using System.Linq;
using System.Reflection;

namespace Nubank.Domain.Operations
{
    public class OperationFactory : IOperationFactory
    {
        private readonly IServiceProvider serviceProvider;
        public OperationFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IOperation CreateOperation(IData data) 
        {
            switch (data.Name)
            {
                case Account.name:
                    return serviceProvider.GetService<IOperation<Account>>().Build(data as Account) as IOperation;
                case Transaction.name:
                    return serviceProvider.GetService<IOperation<Transaction>>().Build(data as Transaction) as IOperation;
                default:
                    throw new Exception("Operation not supported");
            }
        }
    }
}
