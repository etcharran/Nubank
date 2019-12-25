using Nubank.Contract;
using Nubank.Persistence.Repositories;

namespace Nubank.Domain.Validation
{
    public class InsufficientLimitValidation : BusinessValidation<Transaction>
    {
        private readonly IAccountRepository accountRepository;
        public const string name = "insufficient-limit";

        public override string ValidationName => name;

        public InsufficientLimitValidation(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public override bool IsValid(Transaction data) => accountRepository.Get().AvailableLimit >= data.Amount;
    }
}
