using Nubank.Contract;
using Nubank.Persistence.Repositories;

namespace Nubank.Domain.Validation
{
    public class InactiveCardValidation : BusinessValidation<Transaction>
    {
        private readonly IAccountRepository accountRepository;
        public const string name = "card-not-active";

        public InactiveCardValidation(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public override string ValidationName => name;


        public override bool IsValid(Transaction data)
        {
            return accountRepository.Get().ActiveCard; ;
        }
    }
}
