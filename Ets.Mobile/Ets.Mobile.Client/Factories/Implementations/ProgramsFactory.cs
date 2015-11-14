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
                CompletedCreditsCount = short.Parse(program.CompletedCreditsCount),
                EquivalenceCount = short.Parse(program.EquivalenceCount),
                FailedCreditsCount = short.Parse(program.FailedCreditsCount),
                Name = program.Name,
                PotentialCreditsCount = short.Parse(program.PotentialCreditsCount),
                RegisteredCreditsCount = short.Parse(program.RegisteredCreditsCount),
                ResearchCreditsCount = short.Parse(program.ResearchCreditsCount),
                SemesterEnd = program.SemesterEnd,
                SemesterStart = program.SemesterStart,
                Status = program.Status,
                SuceededCreditsCount = short.Parse(program.SuceededCreditsCount)
            }));
        }
    }
}
