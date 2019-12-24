using Nubank.Contract;

namespace Nubank.Domain.Validation
{
    public abstract class BusinessValidation<T> : IBusinessValidation<T> where T : Data
    {
        public abstract string ValidationName { get; }

        public ValidationResponse Validate(T data) => new ValidationResponse { Success = IsValid(data), Validation = ValidationName };

        public abstract bool IsValid(T data);
    }
}
