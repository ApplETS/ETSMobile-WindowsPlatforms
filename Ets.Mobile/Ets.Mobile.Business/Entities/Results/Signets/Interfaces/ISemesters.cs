using Ets.Mobile.Business.Entities.Signets;
using System.Collections.Generic;

namespace Ets.Mobile.Business.Entities.Results.Signets.Interfaces
{
    public interface ISemesters
    {
        List<Semester> Semesters { get; set; }
        string ErrorMessage { get; set; }
    }
}