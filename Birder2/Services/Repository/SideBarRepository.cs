﻿using Birder2.Data;
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
        //ToDo: Craete new method in UserAccessor to return userId, not full user
        public async Task<int> TotalObservationsCount(ApplicationUser user)
        {
            return await (from observations in _dbContext.Observations
                            where (observations.ApplicationUserId == user.Id)
                                select observations).CountAsync();
        }

        public async Task<int> UniqueSpeciesCount(ApplicationUser user)
        {
            return await (from observations in _dbContext.Observations
                             where(observations.ApplicationUserId == user.Id)
                                select observations.BirdId).Distinct().CountAsync();
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

        public async Task<IQueryable<TopObservationsViewModel>> GetTopObservations(ApplicationUser user)
        {
            return (from observations in _dbContext.Observations
                 .Include(b => b.Bird)
                 .Where(u => u.ApplicationUser.Id == user.Id)
                    group observations by observations.Bird into species
                    select new TopObservationsViewModel
                    {
                        Vernacular = species.FirstOrDefault().Bird.EnglishName,
                        Count = species.Count()
                    }).Take(5);
        }

        public async Task<IQueryable<TopObservationsViewModel>> GetTopObservations(ApplicationUser user, DateTime date)
        {
            DateTime endDate = date.AddDays(-30);
            return (from observations in _dbContext.Observations
                    .Include(b => b.Bird)
                        where observations.ApplicationUserId == user.Id && (observations.ObservationDateTime >= endDate && observations.ObservationDateTime <= date)
                            group observations by observations.Bird into species
                                select new TopObservationsViewModel
                                {
                                    Vernacular = species.FirstOrDefault().Bird.EnglishName,
                                    Count = species.Count()
                                }).Take(5);
        }

    }
}