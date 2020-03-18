using Nubank.Contract;

namespace Nubank.Domain.Operations
{
    public interface IOperationLogic
    {
        /// <summary>
        /// Executes the operation matching the data type 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        IResponse<Account> Process<T>(T data) where T : IData;
    }
}
