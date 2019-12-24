using Nubank.Contract;

namespace Nubank.Domain.Validation
{
    public interface IBusinessValidation<T> where T : IData
    {
        string ValidationName { get; }
        ValidationResponse Validate(T data);
    }
}