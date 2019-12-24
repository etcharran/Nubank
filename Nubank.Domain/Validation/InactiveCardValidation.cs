using Nubank.Contract;
using Nubank.Persistence.Repositories;

namespace Nubank.Domain.Validation
{
    public class InactiveCardValidation : BusinessValidation<Transaction>
    {
        private readonly IAccountRepository accountRepository;
        public override string ValidationName => "card-not-active";

        public InactiveCardValidation(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public override bool IsValid(Transaction data) => accountRepository.Get().ActiveCard;
    }
}
