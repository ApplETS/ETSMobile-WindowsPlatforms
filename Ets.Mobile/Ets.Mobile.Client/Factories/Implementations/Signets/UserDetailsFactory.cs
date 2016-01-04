using Ets.Mobile.Business.Entities.Results.Signets;
using Ets.Mobile.Client.Factories.Interfaces.Signets;
using Ets.Mobile.Entities.Signets;

namespace Ets.Mobile.Client.Factories.Implementations.Signets
{
    public class UserDetailsFactory : IUserDetailsFactory
    {
        public UserDetailsVm Create(UserDetailsResult result)
        {
            return new UserDetailsVm
            {
                Balance = result.Balance,
                LastName = result.LastName,
                FirstName = result.FirstName,
                IsMan = result.IsMan,
                PermanentCode = result.PermanentCode
            };
        }
    }
}