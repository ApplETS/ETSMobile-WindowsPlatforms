using Ets.Mobile.Business.Contracts;
using System.Threading.Tasks;

namespace Ets.Mobile.Business.Shared.Tests.Contracts
{
    public interface ISignetsBusinessServiceTest
    {
        Task<ISignetsBusinessService> GetSignetsServices();
        Task TestDonneesAuthentificationValides();
        Task TestInfoEtudiant();
        Task TestLireHoraireDesSeances();
        Task TestLireJoursRemplaces();
        Task TestListeCours();
        Task TestListeElementsEvaluation();
        Task TestListeHoraireEtProf();
        Task TestListeProgrammes();
        Task TestListeSessions();
    }
}