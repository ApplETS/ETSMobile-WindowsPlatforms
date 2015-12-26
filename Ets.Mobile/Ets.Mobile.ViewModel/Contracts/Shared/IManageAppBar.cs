using System;
using System.Reactive;

namespace Ets.Mobile.ViewModel.Contracts.Shared
{
    public interface IManageAppBar
    {
        IObservable<Unit> ShowAppBar();
        IObservable<Unit> HideAppBar();
    }
}