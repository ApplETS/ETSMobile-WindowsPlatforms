using Ets.Mobile.Business.Entities.Results.Signets;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ets.Mobile.Business.Contracts
{
    public interface ISignetsBusinessService
    {
        [Post("/donneesAuthentificationValides")]
        Task<LoginResult> LoginRaw([Body] Dictionary<string, string> form);

        [Post("/infoEtudiant")]
        Task<UserDetailsResult> UserDetailsRaw([Body] Dictionary<string, string> form);

        [Post("/listeCours")]
        Task<CoursesResult> CoursesRaw([Body] Dictionary<string, string> form);

        [Post("/listeCoursIntervalleSessions")]
        Task<CoursesIntervalSemesterResult> CoursesIntervalSemesterRaw([Body] Dictionary<string, string> form);

        [Post("/listeSessions")]
        Task<SemestersResult> SemestersRaw([Body] Dictionary<string, string> form);

        [Post("/listeProgrammes")]
        Task<ProgramsResult> ProgramsRaw([Body] Dictionary<string, string> form);

        [Post("/listeCoequipiers")]
        Task<TeammatesResult> TeammatesRaw([Body] Dictionary<string, string> form);

        [Post("/listeElementsEvaluation")]
        Task<EvaluationsResult> EvaluationsRaw([Body] Dictionary<string, string> form);

        [Post("/listeHoraireEtProf")]
        Task<ScheduleAndTeachersResult> ScheduleAndTeachersRaw([Body] Dictionary<string, string> form);

        [Post("/listeHoraireExamensFin")]
        Task<ScheduleFinalExamsResult> ScheduleFinalExamsRaw([Body] Dictionary<string, string> form);

        [Post("/lireHoraire")]
        Task<CourseForSemesterResult> CoursesForSemesterRaw([Body] Dictionary<string, string> form);

        [Post("/lireJoursRemplaces")]
        Task<ReplacedDaysResult> ReplacedDaysRaw([Body] Dictionary<string, string> form);

        [Post("/lireHoraireDesSeances")]
        Task<ScheduleResult> ScheduleRaw([Body] Dictionary<string, string> form);
    }
}