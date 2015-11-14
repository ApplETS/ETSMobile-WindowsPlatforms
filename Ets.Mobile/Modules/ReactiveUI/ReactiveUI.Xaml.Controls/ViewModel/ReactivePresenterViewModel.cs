using System;
using System.Linq;
using System.Reactive.Linq;

namespace ReactiveUI.Xaml.Controls.ViewModel
{
    public class ReactivePresenterViewModel<T> : IReactivePresenterViewModel<T>
    {
        public static IReactivePresenterViewModel<T> Create<T>(
            IObservable<T> value, 
            IObservable<bool> refreshObservable = null, 
            IObservable<bool> isEmptyObservable = null,
            IObservable<Exception> exceptionObservable = null)
        {
            var presenter = new ReactivePresenterViewModel<T>
            {
                Content = value.Select(x => (object) x),
                IsRefreshing = refreshObservable ?? Observable.Never<bool>(),
                IsContentEmpty = isEmptyObservable ?? Observable.Never<bool>(),
                ThrownExceptions = exceptionObservable ?? Observable.Never<Exception>()
            };

            return presenter;
        }

        public static IReactivePresenterViewModel<ReactiveList<T>> Create<T>(
            IReactiveList<T> value,
            IObservable<bool> refreshObservable = null,
            IObservable<Exception> exceptionObservable = null)
        {
            var presenter = new ReactivePresenterViewModel<ReactiveList<T>>
            {
                Content = value.Changed.Where(x => value.Any()).Select(x => value),
                IsContentEmpty = value.IsEmptyChanged.CombineLatest(value.Changed, (isEmpty, changed) => value.IsEmpty),
                IsRefreshing = refreshObservable ?? Observable.Never<bool>(),
                ThrownExceptions = exceptionObservable ?? Observable.Never<Exception>()
            };

            return presenter;
        }

        public static IReactivePresenterViewModel<ReactiveList<T>> Create<T, TTile>(
            IReactiveList<T> value,
            IReactiveDerivedList<TTile> derived,
            IObservable<bool> refreshObservable = null,
            IObservable<Exception> exceptionObservable = null)
        {
            var presenter = new ReactivePresenterViewModel<ReactiveList<T>>
            {
                Content = derived.Changed.Where(x => derived.Any()).Select(x => derived),
                IsContentEmpty = derived.IsEmptyChanged.CombineLatest(derived.Changed, (isEmpty, changed) => derived.IsEmpty), // value.IsEmptyChanged.CombineLatest(value.Changed, (b1, b2) => b1),
                IsRefreshing = refreshObservable ?? Observable.Never<bool>(),
                ThrownExceptions = exceptionObservable ?? Observable.Never<Exception>()
            };

            return presenter;
        }

        public IObservable<object> Content { get; set; }
        public IObservable<bool> IsRefreshing { get; set; }
        public IObservable<bool> IsContentEmpty { get; set; }
        public IObservable<Exception> ThrownExceptions { get; set; }
    }

    public static class ReactivePresenterBuilderExtensions
    {
        /// <summary>
        /// Populate the IObservable to Observe the object
        /// when Empty
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="isEmptyChangedFunc"></param>
        /// <returns></returns>
        public static IReactivePresenterViewModel<T> Empty<T>(this IReactivePresenterViewModel<T> builder, Func<IObservable<bool>> isEmptyChangedFunc)
        {
            builder.IsContentEmpty = isEmptyChangedFunc();
            return builder;
        }

        /// <summary>
        /// Populate the IObservable to Observe the object
        /// when Refreshing 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="refreshingFunc"></param>
        /// <returns></returns>
        public static IReactivePresenterViewModel<T> Refresh<T>(this IReactivePresenterViewModel<T> builder, Func<IObservable<bool>> refreshingFunc)
        {
            builder.IsRefreshing = refreshingFunc();
            return builder;
        }

        /// <summary>
        /// Populate the IObservable to Observe the object
        /// when there's an error occuring
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="errorFunc"></param>
        /// <returns></returns>
        public static IReactivePresenterViewModel<T> Error<T>(this IReactivePresenterViewModel<T> builder, Func<IObservable<Exception>> errorFunc)
        {
            builder.ThrownExceptions = errorFunc();
            return builder;
        }

        /// <summary>
        /// Populate the presenter
        /// with available IObservables
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <param name="reactiveCommandFunc"></param>
        /// <returns></returns>
        public static IReactivePresenterViewModel<T> HandleReactiveCommand<T>(this IReactivePresenterViewModel<T> builder, Func<ReactiveCommand<T>> reactiveCommandFunc)
        {
            builder.IsRefreshing = reactiveCommandFunc().IsExecuting;
            builder.ThrownExceptions = reactiveCommandFunc().ThrownExceptions;
            return builder;
        }
    }
}
