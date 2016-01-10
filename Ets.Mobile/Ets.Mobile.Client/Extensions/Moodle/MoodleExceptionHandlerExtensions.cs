using Ets.Mobile.Client.Contracts;
using System.Linq;
using System.Reactive.Linq;

namespace Ets.Mobile.Client.Extensions.Moodle
{
    public static class MoodleExceptionHandlerExtensions
    {
        public static void HandleError(this IMoodleService This, string exceptionMessage, params string[] strings)
        {
            strings?.Where(str => !string.IsNullOrEmpty(str)).ToObservable().Do(x =>
            {
                throw new MoodleException(exceptionMessage);
            });
        }

        public static void HandleError(this IMoodleService This, object[] objects, string exceptionMessage)
        {
            if (objects == null || objects.Length == 0)
            {
                throw new MoodleException(exceptionMessage);
            }
        }
    }
}