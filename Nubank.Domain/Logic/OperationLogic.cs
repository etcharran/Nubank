using Microsoft.Extensions.Logging;
using Nubank.Contract;
using Nubank.Domain.Operations;
using System.Text.Json;

namespace Nubank.Domain.Logic
{
    public class OperationLogic : IOperationLogic
    {
        private readonly ILogger logger;
        private readonly IOperationFactory operationFactory;

        public OperationLogic(ILogger logger, IOperationFactory operationFactory)
        {
            this.operationFactory = operationFactory;
            this.logger = logger;
        }

        public void Operate(IData operation)
        {
            operationFactory.CreateOperation(operation).Process();
        }
    }
}