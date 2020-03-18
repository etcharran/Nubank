using Nubank.Contract;
using Nubank.Domain.Validation;
using Nubank.Persistence.Repositories;
using System;

namespace Nubank.Domain.Operations
{
    public class TransactionOperation : Operation<Transaction>
    {
        private readonly ITransactionRepository transactionRepository;
        public TransactionOperation(IAccountRepository accountRepository, ITransactionRepository transactionRepository, IServiceProvider provider)
            : base(accountRepository, provider)
        {
            this.transactionRepository = transactionRepository;
        }


        public override void Execute()
        {
            transactionRepository.Create(Data);
            var account = accountRepository.Get();
            account.AvailableLimit -= Data.Amount;
            accountRepository.Update(account);
        }

        public override void InitializeFixture()
        {
            AddToFixture<InsufficientLimitValidation>();
            AddToFixture<InactiveCardValidation>();
            AddToFixture<HighFrequencyValidation>();
            AddToFixture<DoubledTransactionValidation>();
        }
    }
}