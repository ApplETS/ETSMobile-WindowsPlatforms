using System.Collections.Generic;
using Ets.Mobile.Business.Entities.Signets;

namespace Ets.Mobile.Business.Entities.Results.Signets.Interfaces
{
    public interface ITeammates
    {
        List<Teammate> Teammates { get; set; }
        string ErrorMessage { get; set; }
    }
}