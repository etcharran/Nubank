using Nubank.Contract;

namespace Nubank.Domain.Validation
{
    /// <summary>
    /// Business Validation over a parameter
    /// </summary>
    /// <typeparam name="T">Input Parameter IData</typeparam>
    public interface IBusinessValidation<T> where T : IData
    {
        /// <summary>
        /// Validation Name
        /// </summary>
        string ValidationName { get; }

        /// <summary>
        /// Actual Validation
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        ValidationResponse Validate(T data);
    }
}