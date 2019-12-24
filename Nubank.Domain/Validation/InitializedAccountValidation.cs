using Nubank.Contract;
using Nubank.Persistence.Repositories;

namespace Nubank.Domain.Validation
{
    public class InitializedAccountValidation : BusinessValidation<Account>
    {
        private readonly IAccountRepository accountRepository;
        public override string ValidationName => "account-already-initialized";

        public InitializedAccountValidation(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public override bool IsValid(Account data) => accountRepository.Get() == null;
    }
}
