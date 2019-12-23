using Nubank.Contract;

namespace Nubank.Persistence.Repositories
{
    public interface IAccountRepository
    {
        void Create(Account data);
        void Delete(Account data);
        Account Get();
        void Update(Account data);
    }
}