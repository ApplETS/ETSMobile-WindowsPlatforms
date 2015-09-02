using System.Collections.Generic;
using System.Linq;
using Ets.Mobile.Business.Entities.Results.Signets;
using Ets.Mobile.Client.Factories.Interfaces;
using Ets.Mobile.Entities.Signets;

namespace Ets.Mobile.Client.Factories.Implementations
{
    public class EvaluationsFactory : IEvaluationsFactory
    {
        public EvaluationsVm Create(EvaluationsResult result)
        {
            return new EvaluationsVm
            {
                ActualGrade = result.ActualGrade ?? 0,
                AverageOfClass = result.AverageOfClass ?? 0,
                ActualGradeOfIndividualElements = result.ActualGradeOfIndividualElements ?? 0,
                Evaluations = new List<EvaluationVm>(result.Evaluations.Select(evaluation => new EvaluationVm
                {
                    Average = evaluation.Average,
                    CourseAndGroup = evaluation.CourseAndGroup,
                    Grade = evaluation.Grade ?? 0,
                    IgnoredFromCalculation = evaluation.IgnoredFromCalculation == "Oui",
                    Median = evaluation.Median,
                    MessageOfTeacher = evaluation.MessageOfTeacher,
                    Name = evaluation.Name,
                    Percentile = evaluation.Percentile,
                    Published = evaluation.Published,
                    StandardDeviation = evaluation.StandardDeviation,
                    TargetDate = evaluation.TargetDate,
                    Team = evaluation.Team,
                    Total = evaluation.Total,
                    Weighting = evaluation.Weighting
                })),
                FinalGradeOnHundred = result.FinalGradeOnHundred ?? 0,
                GradeOnHundredOfIndividualElements = result.GradeOnHundredOfIndividualElements ?? 0,
                MedianOfClass = result.MedianOfClass ?? 0,
                PercentileOfClass = result.PercentileOfClass ?? 0,
                StandardDeviationOfClass = result.StandardDeviationOfClass ?? 0
            };
        }
    }
}
