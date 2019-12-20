using Microsoft.Extensions.Logging;
using Nubank.Contract;

namespace Nubank.Domain.Operations
{
    public class AccountOperation : Operation<Account>, IOperation, IOperation<Account>
    {
        private readonly ILogger logger;
        public AccountOperation(ILogger logger) {
            this.logger = logger;
        }

        public override void Execute()
        {
            logger.LogInformation($"Process Transaction. ActiveCard: {Data.ActiveCard}, AvailableLimit: {Data.AvailableLimit}");
        }
    }
}