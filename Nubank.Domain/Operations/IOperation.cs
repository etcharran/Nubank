using Nubank.Contract;
namespace Nubank.Domain.Operations
{
    public interface IOperation<T>: IOperation where T: IData
    {
        IOperation<T> Build(T data);
    }

    public interface IOperation
    {
        void Process();
    }
}
