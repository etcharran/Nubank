using Nubank.Contract;

namespace Nubank.Persistence
{
    public interface IRepository<T> where T:IData, ICLonable<T>
    {
        T Get();
        T[] GetAll();
        void Create(T data);
        void Update(T data);
        void Delete(T data);

    }
}