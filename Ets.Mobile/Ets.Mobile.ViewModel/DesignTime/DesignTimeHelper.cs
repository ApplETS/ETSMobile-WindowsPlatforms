using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Messaging.Interfaces.Common;
using ReactiveUI;
using ReactiveUI.Xaml.Controls.Handlers;

namespace Ets.Mobile.ViewModel.DesignTime
{
    public class DesignTimeBase : INotifyPropertyChanged
    {
        public static bool IsInDesignMode => DesignMode.DesignModeEnabled;

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

    public class ReactivePresenterHandlerDesignTime<T> : IReactivePresenterHandler<T>
    {
        public ReactivePresenterHandlerDesignTime(object value)
        {
            Content = Observable.Return(value);
            _value = value;
        }

        private readonly object _value;

        public void OnNextValue(T obj) {}
        public void OnNextIsReady(bool isReady) {}

        public IObservable<object> Content { get; set; }
        public ISubject<bool> IsReady { get; set; }
        public IObservable<bool> IsRefreshing { get; set; }
        public IObservable<IMessagingContent> EmptyMessage { get; set; }
        public IObservable<Exception> ThrownExceptions { get; set; }
        public Task<object> GetLastValue()
        {
            return Task.FromResult(_value);
        }

        private class MessagingContent : IMessagingContent
        {
            public string Title { get; }
            public string Message { get; }
        }

        public Task<IMessagingContent> GetLastEmptyMessage()
        {
            return Task.FromResult(new MessagingContent() as IMessagingContent);
        }

        public Task<Exception> GetLastThrownException()
        {
            return Task.FromResult(new Exception("Exception"));
        }

        public Task<bool> GetLastRefreshing()
        {
            return Task.FromResult(true);
        }

        public void Dispose()
        {
        }
    }

    public class ReactiveDerivedListDesignTime<T> : ReactiveObject, IReactiveDerivedList<T>
    {
        private readonly IEnumerable<T> _enumerable; 

        public ReactiveDerivedListDesignTime(IEnumerable<T> list)
        {
            _enumerable = list;
        }

        public ReactiveDerivedListDesignTime(T[] list)
        {
            _enumerable = list;
        }

        public ReactiveDerivedListDesignTime(List<T> list)
        {
            _enumerable = list;
        }

        public ReactiveDerivedListDesignTime(ReactiveList<T> list)
        {
            _enumerable = list;
        }

        public void Reset()
        {
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _enumerable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _enumerable.GetEnumerator();
        }

        public int Count => _enumerable.Count();
        public IObservable<T> ItemsAdded { get; }
        public IObservable<T> BeforeItemsAdded { get; }
        public IObservable<T> ItemsRemoved { get; }
        public IObservable<T> BeforeItemsRemoved { get; }
        public IObservable<IMoveInfo<T>> BeforeItemsMoved { get; }
        public IObservable<IMoveInfo<T>> ItemsMoved { get; }
        public IObservable<NotifyCollectionChangedEventArgs> Changing { get; }
        public IObservable<NotifyCollectionChangedEventArgs> Changed { get; }
        public IObservable<int> CountChanging { get; }
        public IObservable<int> CountChanged { get; }
        public IObservable<bool> IsEmptyChanged { get; }
        public IObservable<Unit> ShouldReset { get; }
        public IObservable<IReactivePropertyChangedEventArgs<T>> ItemChanging { get; }
        public IObservable<IReactivePropertyChangedEventArgs<T>> ItemChanged { get; }
        public bool ChangeTrackingEnabled { get; set; }
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanging;

        public T this[int index]
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsEmpty { get; }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}