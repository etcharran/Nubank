using Nubank.Contract;
using Nubank.Domain.Validation;
using Nubank.Persistence.Repositories;
using System;

namespace Nubank.Domain.Operations
{
    public class AccountOperation : Operation<Account>
    {
        public AccountOperation(IAccountRepository accountRepository, IServiceProvider provider)
            : base(accountRepository, provider)
        {
        }

        public override void InitializeFixture()
        {
            AddToFixture<InitializedAccountValidation>();
        }

        public override void Execute(Account data) => accountRepository.Create(data);
    }
}