using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Text;
using System.Threading;

namespace Ets.Mobile.ViewModel
{
    public interface IViewModelBase
    {
        CompositeDisposable Subscriptions { get; }
        CancellationToken CancellationToken { get; }
    }
}
