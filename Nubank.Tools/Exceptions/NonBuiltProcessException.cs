using System;

namespace Nubank.Tools.Exceptions
{
    public class NonBuiltProcessException : Exception
    {
        public override string Message => "Non Built Process";
    }
}
