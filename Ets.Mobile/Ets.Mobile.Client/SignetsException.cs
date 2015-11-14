using System;

namespace Ets.Mobile.Client
{
    public class SignetsException : Exception
    {
        public SignetsException(string message) : base(message)
        {
        }

        public SignetsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
