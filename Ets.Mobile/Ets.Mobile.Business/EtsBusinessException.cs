using System;

namespace Ets.Mobile.Business
{
    public class EtsBusinessException : Exception
    {
        public EtsBusinessException(string message) : base(message) { }
    }
}