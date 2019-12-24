using Microsoft.Extensions.Logging;
using Nubank.Contract;
using Nubank.Domain.Validation;
using Nubank.Persistence.Repositories;

namespace Nubank.Domain.Operations
{
    public class AccountOperation : Operation<Account>
    {
        private readonly ILogger logger;

        public AccountOperation(ILogger logger, IAccountRepository repository)
            : base(repository)
        {
            this.logger = logger;
        }

        public override void InitializeFixture()
        {
            ValidationFixture.Add(new InitializedAccountValidation(accountRepository) as IBusinessValidation<Account>);
        }

        public override void Execute() => accountRepository.Create(Data);
    }
}