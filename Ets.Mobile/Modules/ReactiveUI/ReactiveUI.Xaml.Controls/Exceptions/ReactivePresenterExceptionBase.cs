using System;

namespace ReactiveUI.Xaml.Controls.Exceptions
{
    public class ReactivePresenterExceptionBase : Exception
    {
        public ReactivePresenterExceptionBase()
        {
        }

        public ReactivePresenterExceptionBase(string message) : base(message)
        {
        }

        public ReactivePresenterExceptionBase(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
