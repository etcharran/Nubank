using Nubank.Contract;
namespace Nubank.Domain.Operations
{
    public interface IOperation<T> : IOperation where T : IData
    {
    }

    public interface IOperation
    {
        IOperation Build(IData data);
        IResponse<Account> Process();
    }
}
