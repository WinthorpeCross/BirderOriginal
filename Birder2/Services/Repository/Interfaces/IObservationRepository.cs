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
        Task<IEnumerable<Observation>> MyObservationsList(string userId);
        Task<IEnumerable<Observation>> MyNetworkObservationsList(string userId);
        Task<Observation> GetObservationDetails(int? id);
        Task<Observation> AddObservation(Observation observation);
        Task<Observation> UpdateObservation(Observation observation);
        Task<bool> ObservationExists(int id);
        Task<Observation> DeleteObservation(int id);
        Task<IQueryable<LifeListViewModel>> GetLifeList(string userId);
    }
}
