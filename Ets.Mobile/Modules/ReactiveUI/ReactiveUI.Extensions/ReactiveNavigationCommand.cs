using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace ReactiveUI.Extensions
{
    public static class ReactiveNavigationCommand
    {
        public static ReactiveCommand<Unit> CreateNavigationAsyncCommand(this RoutingState router, object viewModelToNavigate)
        {
            return ReactiveCommand.CreateAsyncTask(_ =>
            {
                router.Navigate.Execute(viewModelToNavigate);
                return Task.FromResult(Unit.Default);
            });
        }

        public static ReactiveCommand<Unit> CreateNavigationAsyncObservableCommand(this RoutingState router, object viewModelToNavigate)
        {
            return ReactiveCommand.CreateAsyncObservable(_ =>
            {
                router.Navigate.Execute(viewModelToNavigate);
                return Observable.Return(Unit.Default);
            });
        }
    }
}