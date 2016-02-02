using Akavache;
using Ets.Mobile.Entities.Signets;
using Security.Contracts;
using Splat;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace Ets.Mobile.Client.Extensions.Signets
{
    public static class SignetsServiceExtensions
    {
        public static async Task<UserDetailsVm> LoadUserImage(this Task<UserDetailsVm> userDetailsVm)
        {
            var udvm = await userDetailsVm;

            udvm.Image = await Locator.Current.GetService<IBlobCache>().LoadImageFromUrl("gravatar",
                            "http://www.gravatar.com/avatar/" +
                            $"{Locator.Current.GetService<ISecurityProvider>().HashMd5(udvm.Email.ToLower())}",
                            true).ToTask();

            return udvm;
        }
    }
}