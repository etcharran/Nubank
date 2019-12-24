using System;

namespace Nubank.Tools.Exceptions
{
    public class UnSupportedOperationException : Exception
    {
        public override string Message => "Unsupported Operation";
    }
}
