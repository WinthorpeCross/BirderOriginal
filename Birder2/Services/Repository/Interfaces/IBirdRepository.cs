using Birder2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Birder2.Services
{
    public interface IBirdRepository
    {
        Task<IEnumerable<Bird>> AllBirdsList();
        Task<IEnumerable<Bird>> AllBirdsList(int birdId);
        Task<IEnumerable<Bird>> CommonBirdsList();
        Task<Bird> GetBirdDetails(int? id);
    }
}
