using Nubank.Contract;
namespace Nubank.Domain.Operations
{
    public interface IOperation<T> where T : IData
    {
        IResponse<Account> Process(T data);
    }
}
