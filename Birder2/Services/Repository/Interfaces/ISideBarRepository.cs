using Birder2.Models;
using Birder2.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Birder2.Services
{
    public interface ISideBarRepository
    {
        Task<int> TotalObservationsCount(ApplicationUser user);
        Task<int> UniqueSpeciesCount(ApplicationUser user);
        Task<TweetDay> GetTweetOfTheDayAsync(DateTime date);
        Task<IQueryable<LifeListViewModel>> GetLifeList(ApplicationUser user);
    }
}
