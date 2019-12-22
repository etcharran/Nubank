using System;
using Microsoft.Extensions.Logging;
using Nubank.Contract;
using Nubank.Persistence;

namespace Nubank.Domain.Operations
{
    public class TransactionOperation : Operation<Transaction>
    {
        private readonly ILogger logger;
        private readonly IRepository<Account> accountRepository;
        public TransactionOperation(ILogger logger, IRepository<Account> accountRepository) {
            this.logger = logger;
            this.accountRepository = accountRepository;
        }

        public override void Execute()
        {
            var account = accountRepository.Get();

            account.AvailableLimit -= 20;

            logger.LogInformation($"temp account: {account.AvailableLimit}");

            logger.LogInformation($"real account: {accountRepository.Get().AvailableLimit}");

            logger.LogInformation($"Process Transaction. Amount: {Data.Amount}, Merchant: {Data.Merchant}");
        }
    }
}