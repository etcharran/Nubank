namespace Nubank.Contract
{
    public interface ICLonable<T> where T:IData
    {
        T Clone();
    }
}