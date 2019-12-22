using Microsoft.Extensions.Logging;
using Nubank.Contract;
using Nubank.Persistence;

namespace Nubank.Domain.Operations
{
    public class AccountOperation : Operation<Account>
    {
        private readonly ILogger logger;
        private readonly IRepository<Account> repository;

        public AccountOperation(ILogger logger, IRepository<Account> repository) {
            this.logger = logger;
            this.repository = repository;
        }

        public override void Execute()
        {
            if(repository.Get() == null) 
            {
                repository.Create(Data);
            }
            else
            {
                logger.LogError("There's already an account in the system");
            }

            logger.LogInformation($"Process Account. ActiveCard: {Data.ActiveCard}, AvailableLimit: {Data.AvailableLimit}");
        }
    }
}