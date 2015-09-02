using System;
using System.Reactive.Disposables;

namespace StoreFramework.Extensions
{
    public static class DisposeExtensions
    {
        public static void DisposeWith(this IDisposable disposable, CompositeDisposable compositeDisposable)
        {
            compositeDisposable.Add(disposable);
        }
    }
}
