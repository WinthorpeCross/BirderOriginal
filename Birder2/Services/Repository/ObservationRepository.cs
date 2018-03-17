﻿using Birder2.Data;
using Birder2.Models;
using Birder2.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 

namespace Birder2.Services
{
    public class ObservationRepository : IObservationRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ObservationRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<SpeciesSummaryViewModel> GetLifeList(string userId)
        {
            return (from observations in _dbContext.Observations
                 .Include(b => b.Bird)
                    //.ThenInclude(f => f.BritishStatus)
                 .Include(b => b.Bird)
                    .ThenInclude(u => u.BirdConserverationStatus)
                 .Where(u => u.ApplicationUser.Id == userId)
                 group observations by observations.Bird into species
                 select new SpeciesSummaryViewModel
                 {
                     Vernacular = species.FirstOrDefault().Bird.EnglishName,
                     ScientificName = species.FirstOrDefault().Bird.Species,
                     PopSize = species.FirstOrDefault().Bird.PopulationSize,
                     BtoStatus = species.FirstOrDefault().Bird.BtoStatusInBritain,
                     ConservationStatus = species.FirstOrDefault().Bird.BirdConserverationStatus.ConservationStatus,
                     Count = species.Count()
                 });
        }

        public async Task<int> TotalObservationsCount(ApplicationUser user)
        {
            return await (from observations in _dbContext.Observations
                          where (observations.ApplicationUserId == user.Id)
                          select observations).CountAsync();
        }

        public async Task<int> UniqueSpeciesCount(ApplicationUser user)
        {
            return await (from observations in _dbContext.Observations
                          where (observations.ApplicationUserId == user.Id)
                          select observations.BirdId).Distinct().CountAsync();
        }

        //ToDo: DRY - This is repeated verbatim in two repositories
        public async Task<IEnumerable<Bird>> AllBirdsList()
        {
            return await _dbContext.Birds.ToListAsync();
        }

        public async Task<Bird> GetSelectedBird(int id)
        {
            return await _dbContext.Birds.SingleOrDefaultAsync(m => m.BirdId == id);
        }

        public IQueryable<Observation> GetUsersObservationsList(string userId)
        {
            var observations = _dbContext.Observations
                .Where(o => o.ApplicationUserId == userId)
                    .Include(au => au.ApplicationUser)
                    .Include(b => b.Bird)
                    .Include(ot => ot.ObservationTags)
                        .ThenInclude(t => t.Tag)
                    .OrderByDescending(d => d.ObservationDateTime)
                    .AsNoTracking();
            return observations;
        }

        public IQueryable<Observation> GetPublicObservationsList()
        {
            var observations = _dbContext.Observations
                    .Include(au => au.ApplicationUser)
                    .Include(b => b.Bird)
                    .Include(ot => ot.ObservationTags)
                        .ThenInclude(t => t.Tag)
                    .OrderByDescending(d => d.ObservationDateTime)
                    .AsNoTracking();
                    //.Take(100);
            return observations;
        }

        public IQueryable<Observation> GetUsersNetworkObservationsList(string userId)
        {
            var loggedinUser = _dbContext.Users
                //.Include(x => x.Followers)
                //    .ThenInclude(x => x.Follower)
                .Include(y => y.Following)
                    .ThenInclude(r => r.ApplicationUser)
                .Where(x => x.Id == userId)
                .FirstOrDefault();

            // PROBLEM WHEN FOLLOWERS = 0 -- cannot append own Id

            var userNetwork = (from p in loggedinUser.Following
                               select p.ApplicationUser.Id.ToString());
            //Therefore changed to less efficient || in LINQ WHERE

            var observations = _dbContext.Observations
                .Where(o => userNetwork.Contains(o.ApplicationUser.Id) || o.ApplicationUser.Id == loggedinUser.Id)
                    .Include(au => au.ApplicationUser)
                    .Include(b => b.Bird)
                    .Include(ot => ot.ObservationTags)
                        .ThenInclude(t => t.Tag)
                            .OrderByDescending(d => d.ObservationDateTime)
                                .AsNoTracking();
            return observations;
        }

        public async Task<Observation> GetObservationDetails(int? id)
        {
            return await _dbContext.Observations
                .Include(b => b.Bird)
                .Include(au => au.ApplicationUser)
                .Include(ot => ot.ObservationTags)
                    .ThenInclude(t => t.Tag)
                        .SingleOrDefaultAsync(m => m.ObservationId == id);
        }

        public async Task<Observation> AddObservation(Observation observation)
        {
            _dbContext.Observations.Add(observation);
            await _dbContext.SaveChangesAsync();
            return(observation);  
        }

        public async Task<Observation> UpdateObservation(Observation observation)
        {
            _dbContext.Observations.Update(observation);
            await _dbContext.SaveChangesAsync();
            return(observation);
        }

        public async Task<bool> ObservationExists(int id)
        {
            return await _dbContext.Observations.AnyAsync(e => e.ObservationId == id);
        }

        public async Task<Observation> DeleteObservation(int id)
        {
            var observation = await _dbContext.Observations.SingleOrDefaultAsync(m => m.ObservationId == id);
            _dbContext.Observations.Remove(observation);
            await _dbContext.SaveChangesAsync();
            return observation;
        }
    }
}
