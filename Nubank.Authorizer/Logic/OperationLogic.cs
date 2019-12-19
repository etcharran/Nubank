using Microsoft.Extensions.Logging;
using Nubank.Authorizer.Operations;
using System.Text.Json;

namespace Nubank.Authorizer.Logic
{
    public class OperationLogic: IOperationLogic
    {
        private readonly ILogger logger;
        private readonly IOperationFactory operationFactory;

        public OperationLogic(ILogger logger, IOperationFactory operationFactory)
        {
            this.operationFactory = operationFactory;
            this.logger = logger;
        }

        public void Operate(JsonDocument operation)
        {
            var iterator = operation.RootElement.EnumerateObject();

            while (iterator.MoveNext())
            {
                string operationName = iterator.Current.Name;
                logger.LogInformation(operationName);

                operationFactory.CreateOperation(operationName).Process();
            }
        }
    }
}