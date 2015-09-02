using System;
using StoreFramework.Controls.Presenter.Exceptions;

namespace Ets.Mobile.ViewModel.Exceptions
{
    public class ScheduleException : ReactivePresenterExceptionBase
    {
        public ScheduleException(string message) : base(message) { }
        public ScheduleException(string message, Exception innerException) : base(message, innerException) { }
    }
}
