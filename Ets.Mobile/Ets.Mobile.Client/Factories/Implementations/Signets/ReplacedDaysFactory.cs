using Ets.Mobile.Business.Entities.Results.Signets;
using Ets.Mobile.Client.Factories.Interfaces.Signets;
using Ets.Mobile.Entities.Signets;
using System.Collections.Generic;
using System.Linq;

namespace Ets.Mobile.Client.Factories.Implementations.Signets
{
    public class ReplacedDaysFactory : IReplacedDaysFactory
    {
        public List<ReplacedDayVm> Create(ReplacedDaysResult result)
        {
            return new List<ReplacedDayVm>(result.ReplacedDays.Select(replacedDay => new ReplacedDayVm
            {
                Description = replacedDay.Description,
                OriginDate = replacedDay.OriginDate,
                TargetDate = replacedDay.TargetDate
            }));
        }
    }
}