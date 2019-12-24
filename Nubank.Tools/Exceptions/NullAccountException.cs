using System;

namespace Nubank.Tools.Exceptions
{
    public class NullAccountException : Exception
    {
        public override string Message => "Null Account";
    }
}
