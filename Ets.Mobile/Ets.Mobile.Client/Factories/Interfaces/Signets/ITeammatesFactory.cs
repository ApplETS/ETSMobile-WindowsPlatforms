using Ets.Mobile.Business.Entities.Results.Signets;
using Ets.Mobile.Client.Factories.Interfaces.Shared;
using Ets.Mobile.Entities.Signets;
using System.Collections.Generic;

namespace Ets.Mobile.Client.Factories.Interfaces.Signets
{
    public interface ITeammatesFactory : IFactory<TeammatesResult, List<TeammateVm>>
    {
    }
}