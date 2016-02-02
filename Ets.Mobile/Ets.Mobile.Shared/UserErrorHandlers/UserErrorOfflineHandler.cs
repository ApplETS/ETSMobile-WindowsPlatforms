using Akavache;
using Ets.Mobile.Agent;
using Localization;
using Messaging.Interfaces.Popup;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Ets.Mobile.UserErrorHandlers
{
    public class UserErrorOfflineHandler
    {
        public static List<Type> ExceptionsHandled = new List<Type>(); 

        public static Func<UserError, Task<RecoveryOptionResult>> Handler = userError =>
        {
            if (userError?.InnerException == null)
            {
                throw new ArgumentNullException("userError", "The exception for the UserError (InnerException) needs to be set in order to know which exception occured.");
            }

            if (ExceptionsHandled.Contains(userError.InnerException.GetType()))
            {
                // Ensure we have the most recent values
                // async is not permitted in a static context, hence using both
                // WaitAll and Task.Run togheter to run synchronously.
                Task.WaitAll(Task.Run(async () => await HandleOfflineTask.SetConnectivityValues()));
                Locator.Current.GetService<IBlobCache>().GetObject<bool>("IsCurrentlyOffline")
                    .Catch(Observable.Return(false))
                    .CombineLatest(
                        Locator.Current.GetService<IBlobCache>().GetObject<bool>("HasUserBeenNotified")
                        .Catch(Observable.Return(false)),
                        (x, y) => x && !y
                    )
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .Subscribe(isUserOfflinehasNotBeenNotified =>
                    {
                        if (isUserOfflinehasNotBeenNotified)
                        {
                            Locator.Current.GetService<IPopupManager>()
                                .ShowMessage(StringResources.NetworkError, StringResources.NetworkTitleError);
                            Locator.Current.GetService<IBlobCache>().InsertObject("HasUserBeenNotified", true);
                        }
                    });
            }

            return Task.FromResult(RecoveryOptionResult.CancelOperation);
        };
    }
}