using Nubank.Contract;
using Nubank.Persistence.Repositories;

namespace Nubank.Domain.Validation
{
    public class InsufficientLimitValidation : BusinessValidation<Transaction>
    {

        private readonly IAccountRepository accountRepository;
        public override string ValidationName => "insufficient-limit";

        public InsufficientLimitValidation(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public override bool IsValid(Transaction data) => accountRepository.Get().AvailableLimit >= data.Amount;
    }
}
