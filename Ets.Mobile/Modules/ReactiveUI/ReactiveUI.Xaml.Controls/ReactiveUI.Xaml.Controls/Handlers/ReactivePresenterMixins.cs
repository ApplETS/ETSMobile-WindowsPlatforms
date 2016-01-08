using JetBrains.Annotations;
using Messaging.Interfaces.Common;
using ReactiveUI.Extensions;
using ReactiveUI.Xaml.Controls.Core;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace ReactiveUI.Xaml.Controls.Handlers
{
    public static class ReactivePresenterMixins
    {
        private class DefaultMessagingContent : IMessagingContent
        {
            public DefaultMessagingContent(string message)
            {
                Title = "";
                Message = message;
            }
            public DefaultMessagingContent(string message, string title)
            {
                Title = title;
                Message = message;
            }
            public string Title { get; }
            public string Message { get; }
        }

        private class ReactivePresenterHandler<T> : IReactivePresenterHandler<T>
        {
            public ReactivePresenterHandler([NotNull] IObservable<T> content, [NotNull] IObservable<bool> isRefreshing, [NotNull] IObservable<IMessagingContent> emptyMessage, [NotNull] IObservable<Exception> thrownErrors)
            {
                CompositeDisposable = new CompositeDisposable();

                EmptyMessage = emptyMessage;
                IsRefreshing = isRefreshing;
                ThrownExceptions = thrownErrors;

                // Content Subject is disposable
                var isRefreshingSubject = new ReplaySubject<bool>(1);
                CompositeDisposable.Add(isRefreshingSubject);
                IsRefreshingSubject = isRefreshingSubject;

                // Content Subject is disposable
                var contentSubject = new ReplaySubject<T>(1);
                CompositeDisposable.Add(contentSubject);
                ContentSubject = contentSubject;

                // Empty Message Subject is disposable
                var emptyMessageSubject = new ReplaySubject<IMessagingContent>(1);
                CompositeDisposable.Add(emptyMessageSubject);
                EmptyMessageSubject = emptyMessageSubject;

                // Thrown Errors Subject is disposable
                var thrownErrorsSubject = new ReplaySubject<Exception>(1);
                CompositeDisposable.Add(thrownErrorsSubject);
                ThrownExceptionSubject = thrownErrorsSubject;

                // IsReady Subject is disposable
                var isReadySubject = new Subject<bool>();
                CompositeDisposable.Add(isReadySubject);
                IsReady = isReadySubject;

                Content = content.Select(x => (object)x);
                CompositeDisposable.Add(Content.Subscribe(y => ContentSubject.OnNext((T)y)));
                CompositeDisposable.Add(EmptyMessage.Subscribe(y => EmptyMessageSubject.OnNext(y)));
                CompositeDisposable.Add(ThrownExceptions.Subscribe(y => ThrownExceptionSubject.OnNext(y)));
                CompositeDisposable.Add(IsRefreshing.Subscribe(y => IsRefreshingSubject.OnNext(y)));
            }

            public void OnNextValue([NotNull] T obj)
            {
                ContentSubject.OnNext(obj);
            }

            public void OnNextIsReady(bool isReady)
            {
                IsReady.OnNext(isReady);
            }

            public void OnNextEmptyMessage()
            {
                EmptyMessageSubject.OnNext(new DefaultMessagingContent(""));
            }

            public void OnNextEmptyMessage(IMessagingContent content)
            {
                EmptyMessageSubject.OnNext(content);
            }

            private ISubject<T> ContentSubject { get; }
            private ISubject<IMessagingContent> EmptyMessageSubject { get; }
            private ISubject<Exception> ThrownExceptionSubject { get; }
            private ISubject<bool> IsRefreshingSubject { get; }
            public IObservable<object> Content { get; set; }
            public ISubject<bool> IsReady { get; set; }
            public IObservable<bool> IsRefreshing { get; set; }
            public IObservable<IMessagingContent> EmptyMessage { get; set; }
            public IObservable<Exception> ThrownExceptions { get; set; }

            public Task<object> GetLastValue()
            {
                return ContentSubject.FirstOrDefaultAsync().Select(x => x as object).ToTask();
            }

            public Task<IMessagingContent> GetLastEmptyMessage()
            {
                return EmptyMessageSubject.FirstOrDefaultAsync().ToTask();
            }

            public Task<Exception> GetLastThrownException()
            {
                return ThrownExceptionSubject.FirstOrDefaultAsync().ToTask();
            }

            public Task<bool> GetLastRefreshing()
            {
                return IsRefreshingSubject.FirstOrDefaultAsync().ToTask();
            }

            public readonly CompositeDisposable CompositeDisposable;

            public void Dispose()
            {
                CompositeDisposable.Dispose();
            }
        }

        public static IReactivePresenterHandler<T> CreateReactivePresenter<T>(this ReactivePresenterCommand<T> command)
            where T : ReactiveObject
        {
            return new ReactivePresenterHandler<T>(command, command.IsExecuting, command.Messages, command.ThrownExceptions);
        }

        public static IDisposable SubscribeCommandToReactiveListAndMerge<T>(ReactivePresenterCommand<List<T>> command, [NotNull] ReactiveList<T> reactiveList, [NotNull] IObserver<bool> isReady)
            where T : ReactiveObject, IMergeableObject<T>
        {
            if (reactiveList == null)
            {
                throw new ArgumentNullException(nameof(reactiveList));
            }

            if (isReady == null)
            {
                throw new ArgumentNullException(nameof(isReady));
            }

            return command.Subscribe(list =>
            {
                list.MergeWith(reactiveList);

                isReady.OnNext(true);
            });
        }

        public static IDisposable SubscribeCommandToReactiveListAndMerge<T>(ReactivePresenterCommand<T[]> command, [NotNull] ReactiveList<T> reactiveList, [NotNull] IObserver<bool> isReady)
            where T : ReactiveObject, IMergeableObject<T>
        {
            if (reactiveList == null)
            {
                throw new ArgumentNullException(nameof(reactiveList));
            }

            if (isReady == null)
            {
                throw new ArgumentNullException(nameof(isReady));
            }

            return command.Subscribe(list =>
            {
                list.MergeWith(reactiveList);

                isReady.OnNext(true);
            });
        }

        public static IDisposable SubscribeCommandToReactiveListAndMerge<T>(ReactivePresenterCommand<IEnumerable<T>> command, [NotNull] ReactiveList<T> reactiveList, [NotNull] IObserver<bool> isReady)
            where T : ReactiveObject, IMergeableObject<T>
        {
            if (reactiveList == null)
            {
                throw new ArgumentNullException(nameof(reactiveList));
            }

            if (isReady == null)
            {
                throw new ArgumentNullException(nameof(isReady));
            }

            return command.Subscribe(enumerable =>
            {
                enumerable.MergeWith(reactiveList);

                isReady.OnNext(true);
            });
        }

        public static IReactivePresenterHandler<ReactiveList<T>> CreateReactivePresenter<T>(this ReactivePresenterCommand<List<T>> command, [NotNull] ReactiveList<T> reactiveList, bool mergeResults = false)
            where T : ReactiveObject, IMergeableObject<T>
        {
            if (reactiveList == null)
            {
                throw new ArgumentNullException(nameof(reactiveList));
            }

            var presenter = new ReactivePresenterHandler<ReactiveList<T>>(Observable.Return(reactiveList), command.IsExecuting, command.Messages, command.ThrownExceptions);

            if (mergeResults)
            {
                presenter.CompositeDisposable.Add(SubscribeCommandToReactiveListAndMerge(command, reactiveList, presenter.IsReady));
            }
            else
            {
                presenter.CompositeDisposable.Add(command.Subscribe(x =>
                {
                    reactiveList.Clear();
                    reactiveList.AddRange(x);
                }));
            }

            return presenter;
        }

        public static IReactivePresenterHandler<ReactiveList<T>> CreateReactivePresenter<T>(this ReactivePresenterCommand<T[]> command, [NotNull] ReactiveList<T> reactiveList, bool mergeResults = false)
            where T : ReactiveObject, IMergeableObject<T>
        {
            if (reactiveList == null)
            {
                throw new ArgumentNullException(nameof(reactiveList));
            }

            var presenter = new ReactivePresenterHandler<ReactiveList<T>>(Observable.Return(reactiveList), command.IsExecuting, command.Messages, command.ThrownExceptions);

            if (mergeResults)
            {
                presenter.CompositeDisposable.Add(SubscribeCommandToReactiveListAndMerge(command, reactiveList, presenter.IsReady));
            }
            else
            {
                presenter.CompositeDisposable.Add(command.Subscribe(x =>
                {
                    reactiveList.Clear();
                    reactiveList.AddRange(x);
                }));
            }

            return presenter;
        }

        public static IReactivePresenterHandler<ReactiveList<T>> CreateReactivePresenter<T>(this ReactivePresenterCommand<IEnumerable<T>> command, [NotNull] ReactiveList<T> reactiveList, bool mergeResults = false)
            where T : ReactiveObject, IMergeableObject<T>
        {
            if (reactiveList == null)
            {
                throw new ArgumentNullException(nameof(reactiveList));
            }

            var presenter = new ReactivePresenterHandler<ReactiveList<T>>(Observable.Return(reactiveList), command.IsExecuting, command.Messages, command.ThrownExceptions);

            if (mergeResults)
            {
                presenter.CompositeDisposable.Add(SubscribeCommandToReactiveListAndMerge(command, reactiveList, presenter.IsReady));
            }
            else
            {
                presenter.CompositeDisposable.Add(command.Subscribe(x =>
                {
                    reactiveList.Clear();
                    reactiveList.AddRange(x);
                }));
            }

            return presenter;
        }

        public static IReactivePresenterHandler<IReactiveDerivedList<T>> CreateReactivePresenter<T>(this ReactivePresenterCommand<List<T>> command, [NotNull] ReactiveList<T> reactiveList, [NotNull] IReactiveDerivedList<T> reactiveDerivedList, [NotNull] bool mergeResults = false)
            where T : ReactiveObject, IMergeableObject<T>
        {
            var presenter = new ReactivePresenterHandler<IReactiveDerivedList<T>>(Observable.Return(reactiveDerivedList), command.IsExecuting, command.Messages, command.ThrownExceptions);

            if (mergeResults)
            {
                presenter.CompositeDisposable.Add(SubscribeCommandToReactiveListAndMerge(command, reactiveList, presenter.IsReady));
            }
            else
            {
                presenter.CompositeDisposable.Add(command.Subscribe(x =>
                {
                    reactiveList.Clear();
                    reactiveList.AddRange(x);
                }));
            }

            return presenter;
        }

        public static IReactivePresenterHandler<IReactiveDerivedList<T>> CreateReactivePresenter<T>(this ReactivePresenterCommand<T[]> command, [NotNull] ReactiveList<T> reactiveList, [NotNull] IReactiveDerivedList<T> reactiveDerivedList, bool mergeResults = false)
            where T : ReactiveObject, IMergeableObject<T>
        {
            if (reactiveList == null)
            {
                throw new ArgumentNullException(nameof(reactiveList));
            }

            if (reactiveDerivedList == null)
            {
                throw new ArgumentNullException(nameof(reactiveDerivedList));
            }

            var presenter = new ReactivePresenterHandler<IReactiveDerivedList<T>>(Observable.Return(reactiveDerivedList), command.IsExecuting, command.Messages, command.ThrownExceptions);

            if (mergeResults)
            {
                presenter.CompositeDisposable.Add(SubscribeCommandToReactiveListAndMerge(command, reactiveList, presenter.IsReady));
            }
            else
            {
                presenter.CompositeDisposable.Add(command.Subscribe(x =>
                {
                    reactiveList.Clear();
                    reactiveList.AddRange(x);
                }));
            }

            return presenter;
        }

        public static IReactivePresenterHandler<IReactiveDerivedList<T>> CreateReactivePresenter<T>(this ReactivePresenterCommand<IEnumerable<T>> command, [NotNull] ReactiveList<T> reactiveList, [NotNull] IReactiveDerivedList<T> reactiveDerivedList, bool mergeResults = false)
            where T : ReactiveObject, IMergeableObject<T>
        {
            if (reactiveList == null)
            {
                throw new ArgumentNullException(nameof(reactiveList));
            }

            if (reactiveDerivedList == null)
            {
                throw new ArgumentNullException(nameof(reactiveDerivedList));
            }

            var presenter = new ReactivePresenterHandler<IReactiveDerivedList<T>>(Observable.Return(reactiveDerivedList), command.IsExecuting, command.Messages, command.ThrownExceptions);

            if (mergeResults)
            {
                presenter.CompositeDisposable.Add(SubscribeCommandToReactiveListAndMerge(command, reactiveList, presenter.IsReady));
            }
            else
            {
                presenter.CompositeDisposable.Add(command.Subscribe(x =>
                {
                    reactiveList.Clear();
                    reactiveList.AddRange(x);
                }));
            }

            return presenter;
        }
    }
}