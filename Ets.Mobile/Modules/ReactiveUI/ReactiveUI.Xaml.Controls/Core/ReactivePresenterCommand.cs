// This class adds functionalities to the existing ReactiveCommand
// https://github.com/reactiveui/ReactiveUI/blob/master/ReactiveUI/ReactiveCommand.cs
//
// Description: The exiplicit Observable (as a property): Messages allows the dev
// to handle a message compared to analyze a thrown exception with numerous if's.

using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Messaging.Interfaces.Common;
using Splat;

namespace ReactiveUI.Xaml.Controls.Core
{
    public static class ReactivePresenterCommand
    {
        /// <summary>
        /// Creates a default ReactivePresenterCommand that has no background action. This
        /// is probably what you want if you were calling the constructor in
        /// previous versions of ReactiveUI
        /// </summary>
        /// <param name="canExecute">An Observable that determines when the
        /// Command can Execute. WhenAny is a great way to create this!</param>
        /// <param name="scheduler">The scheduler to deliver events on.
        /// Defaults to RxApp.MainThreadScheduler.</param>
        /// <returns>A ReactivePresenterCommand whose ExecuteAsync just returns the
        /// CommandParameter immediately. Which you should ignore!</returns>
        public static ReactivePresenterCommand<object> Create(IObservable<bool> canExecute = null, IScheduler scheduler = null)
        {
            canExecute = canExecute ?? Observable.Return(true);
            return new ReactivePresenterCommand<object>(canExecute, x => Observable.Return(x), scheduler);
        }

        /// <summary>
        /// Creates a ReactivePresenterCommand typed to the given executeAsync Observable
        /// method. Use this method if your background method returns IObservable.
        /// </summary>
        /// <param name="canExecute">An Observable that determines when the
        /// Command can Execute. WhenAny is a great way to create this!</param>
        /// <param name="executeAsync">Method to call that creates an Observable
        /// representing an operation to execute in the background. The Command's
        /// CanExecute will be false until this Observable completes. If this
        /// Observable terminates with OnError, the Exception is marshaled to
        /// ThrownExceptions.</param>
        /// <param name="scheduler">The scheduler to deliver events on.
        /// Defaults to RxApp.MainThreadScheduler.</param>
        /// <returns>A ReactivePresenterCommand which returns all items that are created via
        /// calling executeAsync as a single stream.</returns>
        public static ReactivePresenterCommand<T> CreateAsyncObservable<T>(IObservable<bool> canExecute, Func<object, IObservable<T>> executeAsync, IScheduler scheduler = null)
        {
            return new ReactivePresenterCommand<T>(canExecute, executeAsync, scheduler);
        }

        /// <summary>
        /// Creates a ReactivePresenterCommand typed to the given executeAsync Observable
        /// method. Use this method if your background method returns IObservable.
        /// </summary>
        /// <param name="executeAsync">Method to call that creates an Observable
        /// representing an operation to execute in the background. The Command's
        /// CanExecute will be false until this Observable completes. If this
        /// Observable terminates with OnError, the Exception is marshaled to
        /// ThrownExceptions.</param>
        /// <param name="scheduler">The scheduler to deliver events on.
        /// Defaults to RxApp.MainThreadScheduler.</param>
        /// <returns>A ReactivePresenterCommand which returns all items that are created via
        /// calling executeAsync as a single stream.</returns>
        public static ReactivePresenterCommand<T> CreateAsyncObservable<T>(Func<object, IObservable<T>> executeAsync, IScheduler scheduler = null)
        {
            return new ReactivePresenterCommand<T>(Observable.Return(true), executeAsync, scheduler);
        }

        /// <summary>
        /// Creates a ReactivePresenterCommand typed to the given executeAsync Task-based
        /// method. Use this method if your background method returns Task or uses
        /// async/await.
        /// </summary>
        /// <param name="canExecute">An Observable that determines when the
        /// Command can Execute. WhenAny is a great way to create this!</param>
        /// <param name="executeAsync">Method to call that creates a Task
        /// representing an operation to execute in the background. The Command's
        /// CanExecute will be false until this Task completes. If this
        /// Task terminates with an Exception, the Exception is marshaled to
        /// ThrownExceptions.</param>
        /// <param name="scheduler">The scheduler to deliver events on.
        /// Defaults to RxApp.MainThreadScheduler.</param>
        /// <returns>A ReactivePresenterCommand which returns all items that are created via
        /// calling executeAsync as a single stream.</returns>
        public static ReactivePresenterCommand<T> CreateAsyncTask<T>(IObservable<bool> canExecute, Func<object, Task<T>> executeAsync, IScheduler scheduler = null)
        {
            return new ReactivePresenterCommand<T>(canExecute, x => executeAsync(x).ToObservable(), scheduler);
        }

        /// <summary>
        /// Creates a ReactivePresenterCommand typed to the given executeAsync Task-based
        /// method. Use this method if your background method returns Task or uses
        /// async/await.
        /// </summary>
        /// <param name="executeAsync">Method to call that creates a Task
        /// representing an operation to execute in the background. The Command's
        /// CanExecute will be false until this Task completes. If this
        /// Task terminates with an Exception, the Exception is marshaled to
        /// ThrownExceptions.</param>
        /// <param name="scheduler">The scheduler to deliver events on.
        /// Defaults to RxApp.MainThreadScheduler.</param>
        /// <returns>A ReactivePresenterCommand which returns all items that are created via
        /// calling executeAsync as a single stream.</returns>
        public static ReactivePresenterCommand<T> CreateAsyncTask<T>(Func<object, Task<T>> executeAsync, IScheduler scheduler = null)
        {
            return new ReactivePresenterCommand<T>(Observable.Return(true), x => executeAsync(x).ToObservable(), scheduler);
        }

        /// <summary>
        /// Creates a ReactivePresenterCommand typed to the given executeAsync Task-based
        /// method. Use this method if your background method returns Task or uses
        /// async/await.
        /// </summary>
        /// <param name="executeAsync">Method to call that creates a Task
        /// representing an operation to execute in the background. The Command's
        /// CanExecute will be false until this Task completes. If this
        /// Task terminates with an Exception, the Exception is marshaled to
        /// ThrownExceptions.</param>
        /// <param name="scheduler">The scheduler to deliver events on.
        /// Defaults to RxApp.MainThreadScheduler.</param>
        /// <returns>A ReactivePresenterCommand which returns all items that are created via
        /// calling executeAsync as a single stream.</returns>
        public static ReactivePresenterCommand<Unit> CreateAsyncTask(Func<object, Task> executeAsync, IScheduler scheduler = null)
        {
            return new ReactivePresenterCommand<Unit>(Observable.Return(true), x => executeAsync(x).ToObservable(), scheduler);
        }

        /// <summary>
        /// Creates a ReactivePresenterCommand typed to the given executeAsync Task-based
        /// method. Use this method if your background method returns Task or uses
        /// async/await.
        /// </summary>
        /// <param name="canExecute">An Observable that determines when the
        /// Command can Execute. WhenAny is a great way to create this!</param>
        /// <param name="executeAsync">Method to call that creates a Task
        /// representing an operation to execute in the background. The Command's
        /// CanExecute will be false until this Task completes. If this
        /// Task terminates with an Exception, the Exception is marshaled to
        /// ThrownExceptions.</param>
        /// <param name="scheduler">The scheduler to deliver events on.
        /// Defaults to RxApp.MainThreadScheduler.</param>
        /// <returns>A ReactivePresenterCommand which returns all items that are created via
        /// calling executeAsync as a single stream.</returns>
        public static ReactivePresenterCommand<Unit> CreateAsyncTask(IObservable<bool> canExecute, Func<object, Task> executeAsync, IScheduler scheduler = null)
        {
            return new ReactivePresenterCommand<Unit>(canExecute, x => executeAsync(x).ToObservable(), scheduler);
        }

        /// <summary>
        /// Creates a ReactivePresenterCommand typed to the given executeAsync Task-based
        /// method that supports cancellation. Use this method if your background
        /// method returns Task or uses async/await.
        /// </summary>
        /// <param name="canExecute">An Observable that determines when the
        /// Command can Execute. WhenAny is a great way to create this!</param>
        /// <param name="executeAsync">Method to call that creates a Task
        /// representing an operation to execute in the background. The Command's
        /// CanExecute will be false until this Task completes. If this
        /// Task terminates with an Exception, the Exception is marshaled to
        /// ThrownExceptions.</param>
        /// <param name="scheduler">The scheduler to deliver events on.
        /// Defaults to RxApp.MainThreadScheduler.</param>
        /// <returns>A ReactivePresenterCommand which returns all items that are created via
        /// calling executeAsync as a single stream.</returns>
        public static ReactivePresenterCommand<T> CreateAsyncTask<T>(IObservable<bool> canExecute, Func<object, CancellationToken, Task<T>> executeAsync, IScheduler scheduler = null)
        {
            return new ReactivePresenterCommand<T>(canExecute, x => Observable.StartAsync(ct => executeAsync(x, ct)), scheduler);
        }

        /// <summary>
        /// Creates a ReactivePresenterCommand typed to the given executeAsync Task-based
        /// method that supports cancellation. Use this method if your background
        /// method returns Task or uses async/await.
        /// </summary>
        /// <param name="executeAsync">Method to call that creates a Task
        /// representing an operation to execute in the background. The Command's
        /// CanExecute will be false until this Task completes. If this
        /// Task terminates with an Exception, the Exception is marshaled to
        /// ThrownExceptions.</param>
        /// <param name="scheduler">The scheduler to deliver events on.
        /// Defaults to RxApp.MainThreadScheduler.</param>
        /// <returns>A ReactivePresenterCommand which returns all items that are created via
        /// calling executeAsync as a single stream.</returns>
        public static ReactivePresenterCommand<T> CreateAsyncTask<T>(Func<object, CancellationToken, Task<T>> executeAsync, IScheduler scheduler = null)
        {
            return new ReactivePresenterCommand<T>(Observable.Return(true), x => Observable.StartAsync(ct => executeAsync(x, ct)), scheduler);
        }

        /// <summary>
        /// Creates a ReactivePresenterCommand typed to the given executeAsync Task-based
        /// method that supports cancellation. Use this method if your background
        /// method returns Task or uses async/await.
        /// </summary>
        /// <param name="canExecute">An Observable that determines when the
        /// Command can Execute. WhenAny is a great way to create this!</param>
        /// <param name="executeAsync">Method to call that creates a Task
        /// representing an operation to execute in the background. The Command's
        /// CanExecute will be false until this Task completes. If this
        /// Task terminates with an Exception, the Exception is marshaled to
        /// ThrownExceptions.</param>
        /// <param name="scheduler">The scheduler to deliver events on.
        /// Defaults to RxApp.MainThreadScheduler.</param>
        /// <returns>A ReactivePresenterCommand which returns all items that are created via
        /// calling executeAsync as a single stream.</returns>
        public static ReactivePresenterCommand<Unit> CreateAsyncTask(Func<object, CancellationToken, Task> executeAsync, IScheduler scheduler = null)
        {
            return new ReactivePresenterCommand<Unit>(Observable.Return(true), x => Observable.StartAsync(ct => executeAsync(x, ct)), scheduler);
        }

        /// <summary>
        /// Creates a ReactivePresenterCommand typed to the given executeAsync Task-based
        /// method that supports cancellation. Use this method if your background
        /// method returns Task or uses async/await.
        /// </summary>
        /// <param name="executeAsync">Method to call that creates a Task
        /// representing an operation to execute in the background. The Command's
        /// CanExecute will be false until this Task completes. If this
        /// Task terminates with an Exception, the Exception is marshaled to
        /// ThrownExceptions.</param>
        /// <param name="scheduler">The scheduler to deliver events on.
        /// Defaults to RxApp.MainThreadScheduler.</param>
        /// <returns>A ReactivePresenterCommand which returns all items that are created via
        /// calling executeAsync as a single stream.</returns>
        public static ReactivePresenterCommand<Unit> CreateAsyncTask(IObservable<bool> canExecute, Func<object, CancellationToken, Task> executeAsync, IScheduler scheduler = null)
        {
            return new ReactivePresenterCommand<Unit>(canExecute, x => Observable.StartAsync(ct => executeAsync(x, ct)), scheduler);
        }

        /// <summary>
        /// This creates a ReactivePresenterCommand that calls several child
        /// ReactivePresenterCommands when invoked. Its CanExecute will match the
        /// combined result of the child CanExecutes (i.e. if any child
        /// commands cannot execute, neither can the parent)
        /// </summary>
        /// <param name="canExecute">An Observable that determines whether the
        /// parent command can execute</param>
        /// <param name="commands">The commands to combine.</param>
        public static ReactivePresenterCommand<object> CreateCombined(IObservable<bool> canExecute, params IReactiveCommand[] commands)
        {
            var childrenCanExecute = commands
                .Select(x => x.CanExecuteObservable)
                .CombineLatest(latestCanExecute => latestCanExecute.All(x => x != false));

            var canExecuteSum = Observable.CombineLatest(
                canExecute.StartWith(true),
                childrenCanExecute,
                (parent, child) => parent && child);

            var ret = ReactivePresenterCommand.Create(canExecuteSum);
            ret.Subscribe(x =>
            {
                foreach (var command in commands)
                {
                    command.Execute(x);
                }
            });
            return ret;
        }

        /// <summary>
        /// This creates a ReactivePresenterCommand that calls several child
        /// ReactivePresenterCommands when invoked. Its CanExecute will match the
        /// combined result of the child CanExecutes (i.e. if any child
        /// commands cannot execute, neither can the parent)
        /// </summary>
        /// <param name="commands">The commands to combine.</param>
        public static ReactivePresenterCommand<object> CreateCombined(params IReactiveCommand[] commands)
        {
            return CreateCombined(Observable.Return(true), commands);
        }
    }

    internal class CanExecuteChangedEventManager : WeakEventManager<ICommand, EventHandler, EventArgs>
    {
    }

    /// <summary>
    /// This class represents a Command that can optionally do a background task.
    /// The results of the background task (or a signal that the Command has been
    /// invoked) are delivered by Subscribing to the command itself, since
    /// ReactivePresenterCommand is itself an Observable. The results of individual
    /// invocations can be retrieved via the ExecuteAsync method.
    /// </summary>
    public class ReactivePresenterCommand<T> : IReactiveCommand<T>, IReactiveCommand
    {
#if NET_45
        public event EventHandler CanExecuteChanged;

        protected virtual void raiseCanExecuteChanged(EventArgs args)
        {
            var handler = this.CanExecuteChanged;
            if (handler != null) {
                handler(this, args);
            }
        }
#else
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (canExecuteDisp == null) canExecuteDisp = canExecute.Connect();
                CanExecuteChangedEventManager.AddHandler(this, value);
            }
            remove { CanExecuteChangedEventManager.RemoveHandler(this, value); }
        }

        protected virtual void raiseCanExecuteChanged(EventArgs args)
        {
            CanExecuteChangedEventManager.DeliverEvent(this, args);
        }
#endif
        readonly Subject<T> executeResults = new Subject<T>();
        readonly Subject<bool> isExecuting = new Subject<bool>();
        readonly Func<object, IObservable<T>> executeAsync;
        readonly IScheduler scheduler;
        readonly ScheduledSubject<Exception> exceptions;

        IConnectableObservable<bool> canExecute;
        bool canExecuteLatest = false;
        IDisposable canExecuteDisp;
        int inflightCount = 0;

        /// <summary>
        /// Don't use this, use ReactivePresenterCommand.CreateXYZ instead
        /// </summary>
        public ReactivePresenterCommand(IObservable<bool> canExecute, Func<object, IObservable<T>> executeAsync, IScheduler scheduler = null)
        {
            this.scheduler = scheduler ?? RxApp.MainThreadScheduler;
            this.executeAsync = executeAsync;

            this.canExecute = canExecute.CombineLatest(isExecuting.StartWith(false), (ce, ie) => ce && !ie)
                .Catch<bool, Exception>(ex => {
                    exceptions.OnNext(ex);
                    return Observable.Return(false);
                })
                .Do(x => {
                    var fireCanExecuteChanged = (canExecuteLatest != x);
                    canExecuteLatest = x;

                    if (fireCanExecuteChanged)
                    {
                        this.raiseCanExecuteChanged(EventArgs.Empty);
                    }
                })
                .Publish();

            if (ModeDetector.InUnitTestRunner())
            {
                this.canExecute.Connect();
            }

            exceptions = new ScheduledSubject<Exception>(CurrentThreadScheduler.Instance, RxApp.DefaultExceptionHandler);
            ThrownExceptions = exceptions.Where(x => !(x is IMessagingContent));
            Messages = exceptions.Where(x => x is IMessagingContent).Select(x => x as IMessagingContent);
        }

        /// <summary>
        /// Executes a Command and returns the result asynchronously. This method
        /// makes it *much* easier to test ReactivePresenterCommand, as well as create
        /// ReactivePresenterCommands who invoke inferior commands and wait on their results.
        ///
        /// Note that you **must** Subscribe to the Observable returned by
        /// ExecuteAsync or else nothing will happen (i.e. ExecuteAsync is lazy)
        ///
        /// Note also that the command will be executed, irrespective of the current value
        /// of the command's canExecute observable.
        /// </summary>
        /// <returns>An Observable representing a single invocation of the Command.</returns>
        /// <param name="parameter">Don't use this.</param>
        public IObservable<T> ExecuteAsync(object parameter = null)
        {
            var ret = Observable.Create<T>(subj => {
                if (Interlocked.Increment(ref inflightCount) == 1)
                {
                    isExecuting.OnNext(true);
                }

                var decrement = new SerialDisposable()
                {
                    Disposable = Disposable.Create(() => {
                        if (Interlocked.Decrement(ref inflightCount) == 0)
                        {
                            isExecuting.OnNext(false);
                        }
                    })
                };

                var disp = executeAsync(parameter)
                    .ObserveOn(scheduler)
                    .Do(
                        _ => { },
                        e => decrement.Disposable = Disposable.Empty,
                        () => decrement.Disposable = Disposable.Empty)
                    .Do(executeResults.OnNext, exceptions.OnNext)
                    .Subscribe(subj);

                return new CompositeDisposable(disp, decrement);
            });

            return ret.Publish().RefCount();
        }


        /// <summary>
        /// Executes a Command and returns the result as a Task. This method
        /// makes it *much* easier to test ReactivePresenterCommand, as well as create
        /// ReactivePresenterCommands who invoke inferior commands and wait on their results.
        /// </summary>
        /// <returns>A Task representing a single invocation of the Command.</returns>
        /// <param name="parameter">Don't use this.</param>
        /// <param name="ct">An optional token that can cancel the operation, if
        /// the operation supports it.</param>
        public Task<T> ExecuteAsyncTask(object parameter = null, CancellationToken ct = default(CancellationToken))
        {
            return ExecuteAsync(parameter).ToTask(ct);
        }

        /// <summary>
        /// Fires whenever an exception would normally terminate ReactiveUI
        /// internal state.
        /// </summary>
        /// <value>The thrown exceptions.</value>
        public IObservable<Exception> ThrownExceptions { get; protected set; }

        /// <summary>
        /// Fires whenever a message has been thrown using Observable.Throw
        /// and sending has a parameter I
        /// </summary>
        /// <value>The thrown message.</value>
        public IObservable<IMessagingContent> Messages { get; protected set; }

        /// <summary>
        /// Returns a BehaviorSubject (i.e. an Observable which is guaranteed to
        /// return at least one value immediately) representing the CanExecute
        /// state.
        /// </summary>
        public IObservable<bool> CanExecuteObservable
        {
            get
            {
                var ret = canExecute.StartWith(canExecuteLatest).DistinctUntilChanged();

                if (canExecuteDisp != null) return ret;

                return Observable.Create<bool>(subj => {
                    var disp = ret.Subscribe(subj);

                    // NB: We intentionally leak the CanExecute disconnect, it's
                    // cleaned up by the global Dispose. This is kind of a
                    // "Lazy Subscription" to CanExecute by the command itself.
                    canExecuteDisp = canExecute.Connect();
                    return disp;
                });
            }
        }

        public IObservable<bool> IsExecuting
        {
            get { return isExecuting.StartWith(inflightCount > 0); }
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return executeResults.Subscribe(observer);
        }

        public bool CanExecute(object parameter)
        {
            if (canExecuteDisp == null) canExecuteDisp = canExecute.Connect();
            return canExecuteLatest;
        }

        /// <summary>
        /// Executes a Command. Note that the command will be executed, irrespective of the current value
        /// of the command's canExecute observable.
        /// </summary>
        public void Execute(object parameter)
        {
            ExecuteAsync(parameter).Catch(Observable.Empty<T>()).Subscribe();
        }

        public virtual void Dispose()
        {
            var disp = Interlocked.Exchange(ref canExecuteDisp, null);
            if (disp != null) disp.Dispose();
        }
    }
}
