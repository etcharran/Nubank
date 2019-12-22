using Nubank.Contract;

namespace Nubank.Persistence
{
    public interface IRepository<T> where T:IData
    {
        T Get();
        void Create(T data);
        void Update(T data);
        void Delete(T data);

    }
}