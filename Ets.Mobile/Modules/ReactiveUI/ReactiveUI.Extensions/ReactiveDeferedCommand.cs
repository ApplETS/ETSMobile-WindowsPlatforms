using System;
using System.Reactive.Linq;

namespace ReactiveUI.Extensions
{
    public static class ReactiveDeferedCommand
    {
        public static ReactiveCommand<T> CreateAsyncObservable<T>(Func<IObservable<T>> command)
        {
            return ReactiveCommand.CreateAsyncObservable(p => Observable.Defer(command));
        }

        public static ReactiveCommand<T> CreateAsyncObservable<T>(Func<object, IObservable<T>> command)
        {
            return ReactiveCommand.CreateAsyncObservable(p => Observable.Defer(() => command(p)));
        }
    }
}
