using System;

namespace Nubank.Tools.Exceptions
{
    public class UnSupportedInputDataException : Exception
    {
        public override string Message => "There is no matching Data in the Contracts.";

        public UnSupportedInputDataException(Exception ex)
            : base(null, ex)
        {
        }

    }
}
