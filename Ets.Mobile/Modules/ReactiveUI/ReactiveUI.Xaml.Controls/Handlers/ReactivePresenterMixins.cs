using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Messaging.Interfaces.Common;
using ReactiveUI.Xaml.Controls.Core;

namespace ReactiveUI.Xaml.Controls.Handlers
{
    public static class ReactivePresenterMixins
    {
        private class ReactivePresenterHandler<T> : IReactivePresenterHandler<T>
        {
            public ReactivePresenterHandler(IObservable<T> content)
            {
                CompositeDisposable = new CompositeDisposable();

                // Content Subject is disposable
                var contentSubject = new Subject<T>();
                CompositeDisposable.Add(contentSubject);
                ContentSubject = contentSubject;

                // IsReady Subject is disposable
                var isReadySubject = new Subject<bool>();
                CompositeDisposable.Add(isReadySubject);
                IsReady = isReadySubject;

                Content = content.Select(x => (object)x);
                CompositeDisposable.Add(Content.Subscribe(y => ContentSubject.OnNext((T)y)));
            }

            public void OnNextValue(T obj)
            {
                ContentSubject.OnNext(obj);
            }

            public void OnNextIsReady(bool isReady)
            {
                IsReady.OnNext(isReady);
            }

            private ISubject<T> ContentSubject { get; }
            public IObservable<object> Content { get; set; }
            public ISubject<bool> IsReady { get; set; }
            public IObservable<bool> IsRefreshing { get; set; }
            public IObservable<IMessagingContent> EmptyMessage { get; set; }
            public IObservable<Exception> ThrownExceptions { get; set; }

            public readonly CompositeDisposable CompositeDisposable;

            public void Dispose()
            {
                CompositeDisposable.Dispose();
            }
        }

        public static IReactivePresenterHandler<T> CreateReactivePresenter<T>(this ReactivePresenterCommand<T> obj)
            where T : ReactiveObject
        {
            var presenter = new ReactivePresenterHandler<T>(obj)
            {
                EmptyMessage = obj.Messages,
                IsRefreshing = obj.IsExecuting,
                ThrownExceptions = obj.ThrownExceptions
            };

            return presenter;
        }

        public static IDisposable SubscribeCommandToReactiveListAndMerge<T>(ReactivePresenterCommand<List<T>> command, ReactiveList<T> reactiveList, IObserver<bool> isReady)
            where T : ReactiveObject, IMergeableObject<T>
        {
            return command.Subscribe(list =>
            {
                // Add
                var itemsToAdd = list.Except(list.Where(x => reactiveList.Contains(x, x))).ToArray();
                if (itemsToAdd.Any())
                {
                    reactiveList.AddRange(itemsToAdd);
                }

                // Remove
                var itemsToRemove = reactiveList.Where(x => !list.Contains(x, x)).ToArray();
                if (itemsToRemove.Any())
                {
                    reactiveList.RemoveAll(itemsToRemove);
                }

                // Merge
                foreach (var item in reactiveList.Where(x => list.Contains(x, x)))
                {
                    var mergeItem = list.First(x => x.Equals(x, item));
                    item.MergeWith(mergeItem);
                }

                isReady.OnNext(true);
            });
        }

        public static IDisposable SubscribeCommandToReactiveListAndMerge<T>(ReactivePresenterCommand<T[]> command, ReactiveList<T> reactiveList, IObserver<bool> isReady)
            where T : ReactiveObject, IMergeableObject<T>
        {
            return command.Subscribe(list =>
            {
                // Add
                var itemsToAdd = list.Except(list.Where(x => reactiveList.Contains(x, x))).ToArray();
                if (itemsToAdd.Any())
                {
                    reactiveList.AddRange(itemsToAdd);
                }

                // Remove
                var itemsToRemove = reactiveList.Where(x => !list.Contains(x, x)).ToArray();
                if (itemsToRemove.Any())
                {
                    reactiveList.RemoveAll(itemsToRemove);
                }

                // Merge
                foreach (var item in reactiveList.Where(x => list.Contains(x, x)))
                {
                    var mergeItem = list.First(x => x.Equals(x, item));
                    item.MergeWith(mergeItem);
                }

                isReady.OnNext(true);
            });
        }

        public static IDisposable SubscribeCommandToReactiveListAndMerge<T>(ReactivePresenterCommand<IEnumerable<T>> command, ReactiveList<T> reactiveList, IObserver<bool> isReady)
            where T : ReactiveObject, IMergeableObject<T>
        {
            return command.Subscribe(enumerable =>
            {
                // Add
                var list = enumerable.ToArray();
                var itemsToAdd = list.Except(list.Where(x => reactiveList.Contains(x, x))).ToArray();
                if (itemsToAdd.Any())
                {
                    reactiveList.AddRange(itemsToAdd);
                }

                // Remove
                var itemsToRemove = reactiveList.Where(x => !list.Contains(x, x)).ToArray();
                if (itemsToRemove.Any())
                {
                    reactiveList.RemoveAll(itemsToRemove);
                }

                // Merge
                foreach (var item in reactiveList.Where(x => list.Contains(x, x)))
                {
                    var mergeItem = list.First(x => x.Equals(x, item));
                    item.MergeWith(mergeItem);
                }

                isReady.OnNext(true);
            });
        }

        public static IReactivePresenterHandler<ReactiveList<T>> CreateReactivePresenter<T>(this ReactivePresenterCommand<List<T>> command, ReactiveList<T> reactiveList, bool mergeResults = false)
            where T : ReactiveObject, IMergeableObject<T>
        {
            var presenter = new ReactivePresenterHandler<ReactiveList<T>>(Observable.Return(reactiveList))
            {
                EmptyMessage = command.Messages,
                IsRefreshing = command.IsExecuting,
                ThrownExceptions = command.ThrownExceptions
            };

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

        public static IReactivePresenterHandler<IReactiveDerivedList<T>> CreateReactivePresenter<T>(this ReactivePresenterCommand<List<T>> command, ReactiveList<T> reactiveList, IReactiveDerivedList<T> reactiveDerivedList, bool mergeResults = false)
            where T : ReactiveObject, IMergeableObject<T>
        {
            var presenter = new ReactivePresenterHandler<IReactiveDerivedList<T>>(Observable.Return(reactiveDerivedList))
            {
                EmptyMessage = command.Messages,
                IsRefreshing = command.IsExecuting,
                ThrownExceptions = command.ThrownExceptions
            };

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

        public static IReactivePresenterHandler<ReactiveList<T>> CreateReactivePresenter<T>(this ReactivePresenterCommand<T[]> command, ReactiveList<T> reactiveList, bool mergeResults = false)
            where T : ReactiveObject, IMergeableObject<T>
        {
            var presenter = new ReactivePresenterHandler<ReactiveList<T>>(Observable.Return(reactiveList))
            {
                EmptyMessage = command.Messages,
                IsRefreshing = command.IsExecuting,
                ThrownExceptions = command.ThrownExceptions
            };

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

        public static IReactivePresenterHandler<IReactiveDerivedList<T>> CreateReactivePresenter<T>(this ReactivePresenterCommand<T[]> command, ReactiveList<T> reactiveList, IReactiveDerivedList<T> reactiveDerivedList, bool mergeResults = false)
            where T : ReactiveObject, IMergeableObject<T>
        {
            var presenter = new ReactivePresenterHandler<IReactiveDerivedList<T>>(Observable.Return(reactiveDerivedList))
            {
                EmptyMessage = command.Messages,
                IsRefreshing = command.IsExecuting,
                ThrownExceptions = command.ThrownExceptions
            };

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

        public static IReactivePresenterHandler<ReactiveList<T>> CreateReactivePresenter<T>(this ReactivePresenterCommand<IEnumerable<T>> command, ReactiveList<T> reactiveList, bool mergeResults = false)
            where T : ReactiveObject, IMergeableObject<T>
        {
            var presenter = new ReactivePresenterHandler<ReactiveList<T>>(Observable.Return(reactiveList))
            {
                EmptyMessage = command.Messages,
                IsRefreshing = command.IsExecuting,
                ThrownExceptions = command.ThrownExceptions
            };

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

        public static IReactivePresenterHandler<IReactiveDerivedList<T>> CreateReactivePresenter<T>(this ReactivePresenterCommand<IEnumerable<T>> command, ReactiveList<T> reactiveList, IReactiveDerivedList<T> reactiveDerivedList, bool mergeResults = false)
            where T : ReactiveObject, IMergeableObject<T>
        {
            var presenter = new ReactivePresenterHandler<IReactiveDerivedList<T>>(Observable.Return(reactiveDerivedList))
            {
                EmptyMessage = command.Messages,
                IsRefreshing = command.IsExecuting,
                ThrownExceptions = command.ThrownExceptions
            };
            
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