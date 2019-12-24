using Microsoft.Extensions.Logging;
using Nubank.Contract;
using Nubank.Domain.Validation;
using Nubank.Persistence.Repositories;

namespace Nubank.Domain.Operations
{
    public class AccountOperation : Operation<Account>
    {
        public AccountOperation(IAccountRepository accountRepository) 
            : base(accountRepository)
        {
        }

        public override void InitializeFixture()
        {
            ValidationFixture.Add(new InitializedAccountValidation(accountRepository));
        }

        public override void Execute() => accountRepository.Create(Data);
    }
}