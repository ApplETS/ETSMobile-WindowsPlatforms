using Newtonsoft.Json;

namespace Ets.Mobile.Business.Entities.Signets
{
    public class Program
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("libelle")]
        public string Name { get; set; }

        [JsonProperty("statut")]
        public string Status { get; set; }

        [JsonProperty("sessionDebut")]
        public string SemesterStart { get; set; }

        [JsonProperty("sessionFin")]
        public string SemesterEnd { get; set; }

        [JsonProperty("moyenne")]
        public string Average { get; set; }

        [JsonProperty("nbEquivalences")]
        public string EquivalenceCount { get; set; }

        [JsonProperty("nbCrsReussis")]
        public string SuceededCreditsCount { get; set; }

        [JsonProperty("nbCrsEchoues")]
        public string FailedCreditsCount { get; set; }

        [JsonProperty("nbCreditsInscrits")]
        public string RegisteredCreditsCount { get; set; }

        [JsonProperty("nbCreditsCompletes")]
        public string CompletedCreditsCount { get; set; }

        [JsonProperty("nbCreditsPotentiels")]
        public string PotentialCreditsCount { get; set; }

        [JsonProperty("nbCreditsRecherche")]
        public string SearchCreditsCount { get; set; }
    }
}
