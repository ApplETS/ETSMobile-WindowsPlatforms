using System;
using Newtonsoft.Json;

namespace Ets.Mobile.Business.Entities.Signets
{
    public class Evaluation
    {
        [JsonProperty("coursGroupe")]
        public string CourseAndGroup { get; set; }
        [JsonProperty("nom")]
        public string Name { get; set; }
        [JsonProperty("equipe")]
        public string Team { get; set; }
        [JsonProperty("dateCible")]
        public DateTime? TargetDate { get; set; }
        [JsonProperty("note")]
        public double? Grade { get; set; }
        [JsonProperty("corrigeSur")]
        public string Total { get; set; }
        [JsonProperty("ponderation")]
        public string Weighting { get; set; }
        [JsonProperty("moyenne")]
        public string Average { get; set; }
        [JsonProperty("ecartType")]
        public string StandardDeviation { get; set; }
        [JsonProperty("mediane")]
        public string Median { get; set; }
        [JsonProperty("rangCentile")]
        public string Percentile { get; set; }
        [JsonProperty("publie")]
        public string Published { get; set; }
        [JsonProperty("messageDuProf")]
        public string MessageOfTeacher { get; set; }
        [JsonProperty("ignoreDuCalcul")]
        public string IgnoredFromCalculation { get; set; }
    }
}
