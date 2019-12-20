using System;
using Microsoft.Extensions.Logging;
using Nubank.Contract;

namespace Nubank.Domain.Operations
{
    public class TransactionOperation : Operation<Transaction>, IOperation, IOperation<Transaction>
    {
        private readonly ILogger logger;
        public TransactionOperation(ILogger logger) {
            this.logger = logger;
        }

        public override void Execute()
        {
            logger.LogInformation($"Process Transaction. Amount: {Data.Amount}, Merchant: {Data.Merchant}");
        }
    }
}