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
                    Total = GetTotal(evaluation.Total),
                    Weighting = evaluation.Weighting
                })),
                FinalGradeOnHundred = result.FinalGradeOnHundred ?? 0,
                GradeOnHundredOfIndividualElements = result.GradeOnHundredOfIndividualElements ?? 0,
                Median = result.Median ?? 0,
                Percentile = result.Percentile ?? 0,
                StandardDeviation = result.StandardDeviation ?? 0
            };
        }

        public double GetTotal(string total)
        {
            if (string.IsNullOrEmpty(total))
            {
                return 0;
            }

            if (total.Contains("+"))
            {
                return total.Split('+').Select(double.Parse).Sum();
            }

            return Convert.ToDouble(total);
        }
    }
}