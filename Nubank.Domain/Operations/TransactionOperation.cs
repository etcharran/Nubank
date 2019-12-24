using Microsoft.Extensions.Logging;
using Nubank.Contract;
using Nubank.Domain.Validation;
using Nubank.Persistence.Repositories;

namespace Nubank.Domain.Operations
{
    public class TransactionOperation : Operation<Transaction>
    {
        private readonly ITransactionRepository transactionRepository;
        public TransactionOperation(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
            : base(accountRepository)
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
            ValidationFixture.Add(new InsufficientLimitValidation(accountRepository));
            ValidationFixture.Add(new InactiveCardValidation(accountRepository));
            ValidationFixture.Add(new HighFrequencyValidation(transactionRepository));
            ValidationFixture.Add(new DoubledTransactionValidation(transactionRepository));
        }
    }
}