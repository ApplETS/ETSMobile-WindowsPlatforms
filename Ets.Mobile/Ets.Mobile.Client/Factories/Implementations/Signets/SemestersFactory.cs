using Ets.Mobile.Business.Entities.Results.Signets;
using Ets.Mobile.Client.Factories.Interfaces.Signets;
using Ets.Mobile.Entities.Signets;
using System.Collections.Generic;
using System.Linq;

namespace Ets.Mobile.Client.Factories.Implementations.Signets
{
    public class SemestersFactory : ISemestersFactory
    {
        public List<SemesterVm> Create(SemestersResult result)
        {
            return new List<SemesterVm>(result.Semesters.Select(semester =>
                new SemesterVm
                {
                    AbridgedName = semester.AbridgedName,
                    Name = semester.Name,
                    EndCancellationDateWithReimbursement = semester.EndCancellationDateWithReimbursement,
                    EndCancellationDateWithReimbursementForNewStudent = semester.EndCancellationDateWithReimbursementForNewStudent,
                    EndCancellationDateWithoutReimbursementForNewStudent = semester.EndCancellationDateWithoutReimbursementForNewStudent,
                    EndDate = semester.EndDate,
                    EndOfClassesDate = semester.EndOfClassesDate,
                    StartDate = semester.StartDate,
                    LimitDateForCancellingAseq = semester.LimitForCancellingASEQ,
                    StartCancellationDateWithReimbursement = semester.StartCancellationDateWithReimbursement,
                    StartCancellationDateWithoutReimbursementForNewStudent = semester.StartCancellationDateWithoutReimbursementForNewStudent
                }));
        }
    }
}