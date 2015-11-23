using System;
using System.Reactive.Subjects;
using Messaging.Interfaces.Common;

namespace ReactiveUI.Xaml.Controls.Handlers
{
    public interface IReactivePresenterHandler
    {
        IObservable<object> Content { get; set; }
        ISubject<bool> IsReady { get; set; }
        IObservable<bool> IsRefreshing { get; set; }
        IObservable<IMessagingContent> EmptyMessage { get; set; }
        IObservable<Exception> ThrownExceptions { get; set; }
    }

    public interface IReactivePresenterHandler<in T> : IReactivePresenterHandler, IDisposable
    {
        void OnNextValue(T obj);
        void OnNextIsReady(bool isReady);
    }
}