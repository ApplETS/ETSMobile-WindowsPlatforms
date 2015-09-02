using System.Collections.Generic;
using System.Linq;
using Ets.Mobile.Business.Entities.Results.Signets;
using Ets.Mobile.Client.Factories.Interfaces;
using Ets.Mobile.Entities.Signets;

namespace Ets.Mobile.Client.Factories.Implementations
{
    public class ProgramsFactory : IProgramsFactory
    {
        public List<ProgramVm> Create(ProgramsResult result)
        {
            return new List<ProgramVm>(result.Programs.Select(program => new ProgramVm
            {
                Average = program.Average,
                Code = program.Code,
                CompletedCreditsCount = program.CompletedCreditsCount,
                EquivalenceCount = program.EquivalenceCount,
                FailedCreditsCount = program.FailedCreditsCount,
                Name = program.Name,
                PotentialCreditsCount = program.PotentialCreditsCount,
                RegisteredCreditsCount = program.RegisteredCreditsCount,
                SearchCreditsCount = program.SearchCreditsCount,
                SemesterEnd = program.SemesterEnd,
                SemesterStart = program.SemesterStart,
                Status = program.Status,
                SuceededCreditsCount = program.SuceededCreditsCount
            }));
        }
    }
}
