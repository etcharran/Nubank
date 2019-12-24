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
        /// Retrieves the last account created
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Nubank.Tools.Exceptions.NullAccountException">If there isn't one throws a nullaccount exeption</exception>
        Account Get();

        /// <summary>
        /// Updates an account
        /// </summary>
        /// <param name="data"></param>
        void Update(Account data);

        /// <summary>
        /// Returns if any account exists
        /// </summary>
        /// <returns></returns>
        bool Any();
    }
}