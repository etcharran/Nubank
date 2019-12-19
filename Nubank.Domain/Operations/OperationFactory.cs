using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Nubank.Domain.Operations
{
    public class OperationFactory : IOperationFactory
    {
        private readonly IServiceProvider serviceProvider;
        public OperationFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IOperation CreateOperation(string operationName)
        {
            var operations = serviceProvider.GetServices<IOperation>();

            return operations.FirstOrDefault(o => o.Name == operationName);
        }
    }
}
