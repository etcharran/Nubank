using Nubank.Contract;

namespace Nubank.Persistence.Repositories
{
    /// <summary>
    /// Persitence layer for managing accounts
    /// </summary>
    public interface IAccountRepository
    {
        /// <summary>
        /// Create an Account
        /// </summary>
        /// <param name="data"></param>
        void Create(Account data);

        /// <summary>
        /// Delete an Account
        /// </summary>
        /// <param name="data"></param>
        void Delete(Account data);

        /// <summary>
        /// Retrieves the last account created if non is created, return null
        /// </summary>
        /// <returns></returns>
        Account Get();

        /// <summary>
        /// Updates an account
        /// </summary>
        /// <param name="data"></param>
        void Update(Account data);
    }
}