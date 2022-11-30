using Microsoft.Extensions.DependencyInjection;
using Nubank.Contract;
using Nubank.Tools.Exceptions;
using System;

namespace Nubank.Domain.Operations
{
    public class OperationLogic : IOperationLogic
    {
        private readonly IServiceProvider provider;
        public OperationLogic(IServiceProvider provider)
        {
            this.provider = provider;
        }

        /// <summary>
        /// Retrieve an operation matching the data type and execute it
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IResponse<Account> Process<T>(T data)
            where T : IData
        {
            var operation = provider.GetService<IOperation<T>>();
            if (operation == null)
                throw new UnSupportedOperationException();

            return operation.Process(data);
        }
    }
}
