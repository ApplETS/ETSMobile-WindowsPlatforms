using System.Collections.Generic;
using Ets.Mobile.Business.Entities.Signets;
using Newtonsoft.Json;
using Ets.Mobile.Business.Entities.Results.Signets.Interfaces;
using Ets.Mobile.Business.Entities.Results.Signets.Converters;

namespace Ets.Mobile.Business.Entities.Results.Signets
{
    [JsonConverter(typeof(GenericConverter))]
    public class EvaluationsResult : ResultBase, IEvaluations
    {
        [JsonProperty("noteACeJour")]
        public double? ActualGrade { get; set; }
        [JsonProperty("scoreFinalSur100")]
        public double? FinalGradeOnHundred { get; set; }
        [JsonProperty("moyenneClasse")]
        public double? AverageOfClass { get; set; }
        [JsonProperty("ecartTypeClasse")]
        public double? StandardDeviationOfClass { get; set; }
        [JsonProperty("medianeClasse")]
        public double? MedianOfClass { get; set; }
        [JsonProperty("rangCentileClasse")]
        public double? PercentileOfClass { get; set; }
        [JsonProperty("noteACeJourElementsIndividuels")]
        public double? ActualGradeOfIndividualElements { get; set; }
        [JsonProperty("noteSur100PourElementsIndividuels")]
        public double? GradeOnHundredOfIndividualElements { get; set; }

        [JsonProperty("liste")]
        public List<Evaluation> Evaluations { get; set; }
    }
}
