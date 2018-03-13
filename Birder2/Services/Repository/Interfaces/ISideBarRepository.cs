using Birder2.Models;
using System;
using System.Threading.Tasks;

namespace Birder2.Services
{
    public interface ISideBarRepository
    {
        Task<int> TotalObservationsCount(ApplicationUser user);
        Task<int> UniqueSpeciesCount(ApplicationUser user);
        Task<TweetDay> GetTweetOfTheDayAsync(DateTime date);
    }
}
