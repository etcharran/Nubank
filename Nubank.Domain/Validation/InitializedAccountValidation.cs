using Nubank.Contract;
using Nubank.Persistence.Repositories;

namespace Nubank.Domain.Validation
{
    public class InitializedAccountValidation : BusinessValidation<Account>
    {
        private readonly IAccountRepository accountRepository;
        public const string name = "account-already-initialized";

        public override string ValidationName => name;

        public InitializedAccountValidation(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public override bool IsValid(Account data) => !accountRepository.Any();
    }
}
