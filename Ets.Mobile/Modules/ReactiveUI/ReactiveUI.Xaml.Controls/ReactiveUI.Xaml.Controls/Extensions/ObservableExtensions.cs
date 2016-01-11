using Messaging.Interfaces.Common;
using System;
using System.Collections;
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
                    if ((x.HasValue && IsValueNullOrEmpty(x.Value) && x.Kind == NotificationKind.OnNext) || (!x.HasValue && x.Exception != null))
                    {
                        return Observable.Throw<T>(new EmptyMessageContent("empty")).Materialize();
                    }
                    return Observable.Return(x);
                })
                .Dematerialize();
        }

        private static bool IsValueNullOrEmpty<T>(T value)
        {
            bool valueNullOrEmpty;

            // Check if the collection is empty
            var reactiveCollection = value as ICollection;
            if (reactiveCollection != null)
            {
                valueNullOrEmpty = reactiveCollection.Count == 0;
            }
            else
            {
                valueNullOrEmpty = value == null;
            }

            return valueNullOrEmpty;
        }
    }
}