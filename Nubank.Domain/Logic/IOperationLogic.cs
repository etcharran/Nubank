using Nubank.Contract;

namespace Nubank.Domain.Logic
{
    public interface IOperationLogic
    {
        IResponse<Account> Operate(IData operation);
    }
}