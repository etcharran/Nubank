using Nubank.Contract;

namespace Nubank.Domain.Validation
{
    public abstract class BusinessValidation<T> : IBusinessValidation<T> where T : Data
    {
        public abstract string ValidationName { get; }

        public ValidationResponse Validate(T data) => new ValidationResponse { Success = IsValid(data), Validation = ValidationName };

        /// <summary>
        /// Condition to be valid
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public abstract bool IsValid(T data);
    }
}
