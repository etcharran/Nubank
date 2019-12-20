using Nubank.Contract;
namespace Nubank.Domain.Operations
{
    public interface IOperation<T> where T: IData
    {
        IOperation<T> Build(T data);
    }

    public interface IOperation
    {
        void Process();
    }
}
