using Nubank.Contract;

namespace Nubank.Domain.Logic
{
    public interface IOperationLogic
    {
        void Operate(IData operation);
    }
}