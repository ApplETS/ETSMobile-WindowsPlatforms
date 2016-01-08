using Ets.Mobile.Business.Entities.Results.Signets;
using Ets.Mobile.Client.Contracts;

namespace Ets.Mobile.Client.Extensions.Signets
{
    public static class SignetsExceptionHandlerExtensions
    {
        public static void HandleError(this ISignetsService service, ResultBase result)
        {
            if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
                throw new SignetsException(result.ErrorMessage);
            }
        }
    }
}