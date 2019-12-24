using Nubank.Contract;

namespace Nubank.Domain.Operations
{
    public interface IOperationFactory
    {
        IOperation CreateOperation(IData data);
    }
}
