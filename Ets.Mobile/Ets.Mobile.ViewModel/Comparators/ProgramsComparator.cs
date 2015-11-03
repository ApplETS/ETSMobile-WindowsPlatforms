using System;
using System.Collections.Generic;
using System.Text;
using Ets.Mobile.Entities.Signets;

namespace Ets.Mobile.ViewModel.Comparators
{
    public class ProgramsComparator : IComparer<ProgramVm>
    {
        public int Compare(ProgramVm x, ProgramVm y)
        {
            if(x.Status == "actif" || x.PotentialCreditsCount > y.PotentialCreditsCount || string.Compare(x.Name, y.Name, StringComparison.Ordinal) < 0)
            {
                return -1;
            }
            if(x.PotentialCreditsCount < y.PotentialCreditsCount || string.Compare(x.Name, y.Name, StringComparison.Ordinal) > 0)
            {
                return 1;
            }

            return 0;
        }
    }
}
