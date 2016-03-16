using Ets.Mobile.Business.Entities.Results.Signets;
using Ets.Mobile.Client.Factories.Interfaces.Signets;
using Ets.Mobile.Entities.Signets;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ets.Mobile.Client.Factories.Implementations.Signets
{
    public class EvaluationsFactory : IEvaluationsFactory
    {
        public EvaluationsVm Create(EvaluationsResult result)
        {
            return new EvaluationsVm
            {
                ActualGrade = result.ActualGrade ?? 0,
                Average = result.Average ?? 0,
                ActualGradeOfIndividualElements = result.ActualGradeOfIndividualElements ?? 0,
                Evaluations = new List<EvaluationVm>(result.Evaluations.Select(evaluation =>
                {
                    var total = GetTotal(evaluation.Total);
                    return new EvaluationVm
                    {
                        Average = evaluation.Average ?? 0,
                        CourseAndGroup = evaluation.CourseAndGroup,
                        Grade = evaluation.Grade ?? 0,
                        IgnoredFromCalculation = evaluation.IgnoredFromCalculation == "Oui",
                        Median = evaluation.Median ?? 0,
                        MessageOfTeacher = evaluation.MessageOfTeacher,
                        Name = evaluation.Name,
                        Percentile = evaluation.Percentile,
                        Published = evaluation.Published,
                        StandardDeviation = evaluation.StandardDeviation,
                        TargetDate = evaluation.TargetDate,
                        Team = evaluation.Team,
                        Total = total,
                        Weighting = evaluation.Weighting ?? 0,
                        // Computed Grades
                        GradeComputed = Math.Round((evaluation.Grade ?? 0)/total*100, 2),
                        AverageComputed = Math.Round((evaluation.Average ?? 0) / total * 100, 2),
                        MedianComputed = Math.Round((evaluation.Median ?? 0) / total * 100, 2)
                    };
                })),
                FinalGradeOnHundred = result.FinalGradeOnHundred ?? 0,
                GradeOnHundredOfIndividualElements = result.GradeOnHundredOfIndividualElements ?? 0,
                Median = result.Median ?? 0,
                Percentile = result.Percentile ?? 0,
                StandardDeviation = result.StandardDeviation ?? 0,
                AverageComputed = Math.Round(result.ActualGradeOfIndividualElements > 0 ? ((result.Average ?? 0) / result.Evaluations.Sum(x => x.Weighting) ?? 1) * 100 : result.Average ?? 0, 2),
                MedianComputed = Math.Round(result.ActualGradeOfIndividualElements > 0 ? ((result.Median ?? 0) / result.Evaluations.Sum(x => x.Weighting) ?? 1) * 100 : result.Median ?? 0, 2)
            };
        }

        public double GetTotal(string total)
        {
            if (string.IsNullOrEmpty(total))
            {
                return 0;
            }

            total = total.Replace(",", ".");

            if (total.Contains("+"))
            {
                return total.Split('+').Select(double.Parse).Sum();
            }

            return Convert.ToDouble(total);
        }
    }
}