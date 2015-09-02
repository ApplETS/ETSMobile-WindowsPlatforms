using System;

namespace StoreFramework.Extensions
{
    public static class ThrownExceptionExtensions
    {
        public static IObservable<Exception> HandleNetworkingException(this IObservable<Exception> This)
        {

            return This;
        }
    }
}
