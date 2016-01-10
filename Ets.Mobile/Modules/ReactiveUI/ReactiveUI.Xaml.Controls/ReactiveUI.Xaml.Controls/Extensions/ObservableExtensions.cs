using Messaging.Interfaces.Common;
using System;
using System.Reactive;
using System.Reactive.Linq;

namespace ReactiveUI.Xaml.Controls.Extensions
{
    public static class ObservableExtensions
    {
        private class EmptyMessageContent : Exception, IMessagingContent
        {
            public EmptyMessageContent(string message) : base(message)
            {
            }
            public string Title { get; } = "";
        }

        public static IObservable<T> ThrowIfEmpty<T>(this IObservable<T> obs)
        {
            return obs
                .FirstOrDefaultAsync()
                .Materialize()
                .SelectMany(x =>
                {
                    if ((x.HasValue && x.Value == null && x.Kind == NotificationKind.OnNext) || (!x.HasValue && x.Exception != null))
                    {
                        return Observable.Throw<T>(new EmptyMessageContent("empty")).Materialize();
                    }
                    return Observable.Return(x);
                })
                .Dematerialize();
        } 
    }
}