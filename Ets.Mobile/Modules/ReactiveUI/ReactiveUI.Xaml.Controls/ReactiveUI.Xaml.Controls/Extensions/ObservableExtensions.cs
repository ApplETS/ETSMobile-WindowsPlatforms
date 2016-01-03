using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Messaging.Interfaces.Common;

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
            return obs.Materialize()
                .SelectMany(x =>
                {
                    if (!x.HasValue)
                    {
                        return Observable.Throw<T>(new EmptyMessageContent("empty")).Materialize();
                    }
                    return Observable.Return(x);
                })
                .Dematerialize();
        } 
    }
}