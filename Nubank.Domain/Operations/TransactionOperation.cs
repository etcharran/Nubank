using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Nubank.Contract;
using Nubank.Domain.Validation;
using Nubank.Persistence;
using Nubank.Persistence.Repositories;

namespace Nubank.Domain.Operations
{
    public class TransactionOperation : Operation<Transaction>
    {
        private readonly ILogger logger;
        private readonly IAccountRepository accountRepository;
        public TransactionOperation(ILogger logger, IAccountRepository accountRepository) {
            this.logger = logger;
            this.accountRepository = accountRepository;
        }

        public override List<IBusinessValidation> ValidationFixture { get; set; }

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