using Birder2.Models;
using Birder2.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Birder2.Services
{
    public interface ISideBarRepository
    {
        Task<TweetDay> GetTweetOfTheDayAsync(DateTime date);
        IQueryable<TopObservationsViewModel> GetTopObservations(ApplicationUser user);
        IQueryable<TopObservationsViewModel> GetTopObservations(ApplicationUser user, DateTime date);
    }
}
