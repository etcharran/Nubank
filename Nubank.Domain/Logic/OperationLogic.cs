using Microsoft.Extensions.Logging;
using Nubank.Contract;
using Nubank.Domain.Operations;

namespace Nubank.Domain.Logic
{
    public class OperationLogic : IOperationLogic
    {
        private readonly IOperationFactory operationFactory;

        public OperationLogic(IOperationFactory operationFactory)
        {
            this.operationFactory = operationFactory;
        }

        public IResponse<Account> Operate(IData operation)
        {
            return operationFactory.CreateOperation(operation).Process();
        }
    }
}