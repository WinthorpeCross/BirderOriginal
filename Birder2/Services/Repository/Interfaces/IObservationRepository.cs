using Birder2.Models;
using Birder2.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Birder2.Services
{
    public interface IObservationRepository
    {
        Task<IEnumerable<Bird>> AllBirdsList();
        Task<Bird> GetSelectedBird(int id);
        IQueryable<Observation> GetUsersObservationsList(string userId);
        IQueryable<Observation> GetUsersNetworkObservationsList(string userId);
        IQueryable<Observation> GetPublicObservationsList();
        Task<Observation> GetObservationDetails(int? id);
        Task<Observation> AddObservation(Observation observation);
        Task<Observation> UpdateObservation(Observation observation);
        Task<bool> ObservationExists(int id);
        Task<Observation> DeleteObservation(int id);
        IQueryable<SpeciesSummaryViewModel> GetLifeList(string userId);
        Task<int> TotalObservationsCount(ApplicationUser user);
        Task<int> UniqueSpeciesCount(ApplicationUser user);
    }
}
