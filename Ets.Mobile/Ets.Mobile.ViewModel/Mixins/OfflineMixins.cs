using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Localization;
using Messaging.UniversalApp.Common;
using Refit;

namespace Ets.Mobile.ViewModel.Mixins
{
    public static class OfflineMixins
    {
        public static void HandleOfflineConnection(this ISubject<Exception> sub, Exception ex)
        {
            var apiException = ex as ApiException;
            if (apiException != null)
            {
                var exceptionMessage = new ErrorMessageContent(apiException.Message, apiException);
                if (apiException.ReasonPhrase == "Not Found")
                {
                    exceptionMessage.Message = StringResources.NetworkError;
                    exceptionMessage.Title = StringResources.NetworkTitleError;
                }
                sub.OnNext(exceptionMessage.Exception);
            }
        }
    }
}