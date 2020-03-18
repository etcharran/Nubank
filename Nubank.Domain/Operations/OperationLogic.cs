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
        public IResponse<Account> Process<T>(T data) where T : IData
        {
            IOperation operation = Create(data);
            operation.Build(data);
            return operation.Process();
        }

        public IOperation Create<T>(T data)
        {
            var operation = provider.GetService(typeof(IOperation<>).MakeGenericType(data.GetType()));
            if (operation == null)
                throw new UnSupportedOperationException();
            return operation as IOperation;
        }
    }
}
