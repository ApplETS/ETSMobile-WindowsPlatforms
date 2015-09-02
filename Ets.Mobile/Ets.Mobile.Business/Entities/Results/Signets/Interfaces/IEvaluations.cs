using System.Collections.Generic;
using Ets.Mobile.Business.Entities.Signets;

namespace Ets.Mobile.Business.Entities.Results.Signets.Interfaces
{
    public interface IEvaluations
    {
        double? ActualGrade { get; set; }
        double? FinalGradeOnHundred { get; set; }
        double? AverageOfClass { get; set; }
        double? StandardDeviationOfClass { get; set; }
        double? MedianOfClass { get; set; }
        double? PercentileOfClass { get; set; }
        double? ActualGradeOfIndividualElements { get; set; }
        double? GradeOnHundredOfIndividualElements { get; set; }
        List<Evaluation> Evaluations { get; set; }
        string ErrorMessage { get; set; }
    }
}