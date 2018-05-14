using Birder2.Data;
using Birder2.Models;
using Birder2.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Birder2.Services
{
    public class SideBarRepository : ISideBarRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SideBarRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TweetDay> GetTweetOfTheDayAsync(DateTime date)
        {
            var tweet = await (from td in _dbContext.TweetDays
                                   .Include(b => b.Bird)
                                        where (td.DisplayDay == date)
                                            select td).FirstOrDefaultAsync();
            if (tweet == null)
            {
                date = new DateTime(2018, 03, 12);
                tweet = await (from td in _dbContext.TweetDays
                                   .Include(b => b.Bird)
                                         where (td.DisplayDay == date)
                                             select td).FirstOrDefaultAsync();
            }
            return tweet;
        }

        public IQueryable<TopObservationsViewModel> GetTopObservations(ApplicationUser user)
        {
            return (from observations in _dbContext.Observations
                 .Include(b => b.Bird)
                 .Where(u => u.ApplicationUser.Id == user.Id)
                    group observations by observations.Bird into species
                    orderby species.Count() descending
                    select new TopObservationsViewModel
                    {
                        Vernacular = species.FirstOrDefault().Bird.EnglishName,
                        Count = species.Count()
                    }).Take(5);
        }

        public IQueryable<TopObservationsViewModel> GetTopObservations(ApplicationUser user, DateTime date)
        {
            DateTime startDate = date.AddDays(-30);
            return (from observations in _dbContext.Observations
                    .Include(b => b.Bird)                                                                     
                        where (observations.ApplicationUserId == user.Id && (observations.ObservationDateTime >= startDate))
                                group observations by observations.Bird into species
                                orderby species.Count() descending
                                    select new TopObservationsViewModel
                                    {
                                        Vernacular = species.FirstOrDefault().Bird.EnglishName,
                                        Count = species.Count()
                                    }).Take(5);
        }

    }
}
