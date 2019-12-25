using Nubank.Contract;

namespace Nubank.Domain.Operations
{
    public interface IOperationFactory
    {
        /// <summary>
        /// Creates an operation through the di
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        IOperation CreateOperation(IData data);
    }
}
