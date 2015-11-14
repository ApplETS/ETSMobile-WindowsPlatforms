using System;

namespace ReactiveUI.Xaml.Controls.ViewModel
{
    public interface IReactivePresenterViewModel
    {
        IObservable<bool> IsRefreshing { get; set; }
        IObservable<bool> IsContentEmpty { get; set; }
        IObservable<Exception> ThrownExceptions { get; set; }
        IObservable<object> Content { get; set; }
    }

    public interface IReactivePresenterViewModel<T> : IReactivePresenterViewModel
    {
    }
}
