using System.Collections.Generic;
using System.Linq;
using Ets.Mobile.Business.Entities.Results.Signets;
using Ets.Mobile.Client.Factories.Interfaces;
using Ets.Mobile.Entities.Signets;

namespace Ets.Mobile.Client.Factories.Implementations
{
    public class TeammatesFactory : ITeammatesFactory
    {
        public List<TeammateVm> Create(TeammatesResult result)
        {
            return new List<TeammateVm>(result.Teammates.Select(teammate => new TeammateVm
            {
                Email = teammate.Email,
                FirstName = teammate.FirstName,
                LastName = teammate.LastName
            }));
        }
    }
}
