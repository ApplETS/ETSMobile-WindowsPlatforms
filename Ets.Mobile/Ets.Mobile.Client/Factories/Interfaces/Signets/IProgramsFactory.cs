using System.Collections.Generic;
using Ets.Mobile.Business.Entities.Results.Signets;
using Ets.Mobile.Client.Factories.Interfaces.Shared;
using Ets.Mobile.Entities.Signets;

namespace Ets.Mobile.Client.Factories.Interfaces.Signets
{
    public interface IProgramsFactory : IFactory<ProgramsResult, List<ProgramVm>>
    {
    }
}