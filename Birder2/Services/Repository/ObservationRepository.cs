using Birder2.Data;
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

        public async Task<IQueryable<LifeListViewModel>> GetLifeList(string userId)
        {
            return  /*IQueryable<LifeListViewModel> model =*/
                (from obs in _dbContext.Observations
                 //.Include(au => au.ApplicationUser)
                 .Include(b => b.Bird)
                 .ThenInclude(f => f.BritishStatus)
                 .Include(b => b.Bird)
                 .ThenInclude(u => u.BirdConserverationStatus)
                 .Where(u => u.ApplicationUser.Id == userId)
                 group obs by obs.Bird into b
                 select new LifeListViewModel
                 {
                     Vernacular = b.FirstOrDefault().Bird.EnglishName,
                     ScientificName = b.FirstOrDefault().Bird.Species,
                     PopSize = b.FirstOrDefault().Bird.PopulationSize,
                     BtoStatus = b.FirstOrDefault().Bird.BritishStatus.BtoStatusInBritain,
                     ConservationStatus = b.FirstOrDefault().Bird.BirdConserverationStatus.ConservationStatus,
                     Count = b.Count(),
                 });
        }

        public async Task<IEnumerable<Bird>> AllBirdsList()
        {
            // ToDo: include Birder category to sort the list by common species
            return await _dbContext.Birds.ToListAsync();
        }

        public async Task<Bird> GetSelectedBird(int id)
        {
            return await _dbContext.Birds.SingleOrDefaultAsync(m => m.BirdId == id);
        }

        public async Task<IEnumerable<Observation>> MyObservationsList(string userId)
        {
            //Change to IQueryable collection - this collection will be refreshed regularly
            var observations = _dbContext.Observations
                    .Where(u => u.ApplicationUser.Id == userId)
                    .Include(au => au.ApplicationUser)
                    .Include(b => b.Bird)
                    .Include(ot => ot.ObservationTags)
                        .ThenInclude(t => t.Tag)
                    .OrderByDescending(d => d.ObservationDateTime)
                    .AsNoTracking()
                    .ToListAsync();
            return await observations;
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
