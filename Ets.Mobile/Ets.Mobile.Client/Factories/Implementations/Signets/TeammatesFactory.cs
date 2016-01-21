using Ets.Mobile.Business.Entities.Results.Signets;
using Ets.Mobile.Client.Factories.Interfaces.Signets;
using Ets.Mobile.Entities.Signets;
using System.Collections.Generic;
using System.Linq;

namespace Ets.Mobile.Client.Factories.Implementations.Signets
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