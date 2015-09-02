using Ets.Mobile.Business.Contracts;
using Ets.Mobile.Business.Entities.Results.Signets;

namespace Ets.Mobile.Client.Extensions.Signets
{
    public static class SignetsExceptionHandlerExtensions
    {
        public static void HandleError(this ISignetsBusinessService service, ResultBase result)
        {
            if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
                throw new SignetsException(result.ErrorMessage);
            }
        }
    }
}
