using System;
using Newtonsoft.Json;

namespace Ets.Mobile.Business.Entities.Signets
{
    public class Semester
    {
        [JsonProperty("abrege")]
        public string AbridgedName { get; set; }

        [JsonProperty("auLong")]
        public string Name { get; set; }

        [JsonProperty("dateDebut")]
        public DateTime StartDate { get; set; }

        [JsonProperty("dateFin")]
        public DateTime EndDate { get; set; }

        [JsonProperty("dateFinCours")]
        public DateTime EndOfClassesDate { get; set; }

        [JsonProperty("dateDebutChemiNot")]
        public DateTime StartCheminot { get; set; }

        [JsonProperty("dateFinChemiNot")]
        public DateTime EndCheminot { get; set; }

        [JsonProperty("dateDebutAnnulationAvecRemboursement")]
        public DateTime StartCancellationDateWithReimbursement { get; set; }

        [JsonProperty("dateFinAnnulationAvecRemboursement")]
        public DateTime EndCancellationDateWithReimbursement { get; set; }
        
        [JsonProperty("dateFinAnnulationAvecRemboursementNouveauxEtudiants")]
        public DateTime EndCancellationDateWithReimbursementForNewStudent { get; set; }

        [JsonProperty("dateDebutAnnulationSansRemboursementNouveauxEtudiants")]
        public DateTime StartCancellationDateWithoutReimbursementForNewStudent { get; set; }

        [JsonProperty("dateFinAnnulationSansRemboursementNouveauxEtudiants")]
        public DateTime EndCancellationDateWithoutReimbursementForNewStudent { get; set; }

        [JsonProperty("dateLimitePourAnnulerASEQ")]
        public DateTime LimitForCancellingASEQ { get; set; }
    }
}
