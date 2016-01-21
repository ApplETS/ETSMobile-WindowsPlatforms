using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using GalaSoft.MvvmLight;

namespace Ets.Mobile.ViewModel.Extensions
{
    public static class ViewModelBaseExensions
    {
        public static async Task RunDisplayTaskAsync<T>(this ViewModelBase vm, Action<T> onNext)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                onNext(default(T));
            });
        }

        public static async void RunDisplayTask<T>(this ViewModelBase vm, Action<T> action)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                action(default(T));
            });
        }

        public static async void RunDisplayTask(this ViewModelBase vm, Action action)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                action();
            });
        }
    }
}
