using Birder2.Data;
using Birder2.Models;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<IEnumerable<Bird>> AllBirdsList()
        {
            return await _dbContext.Birds.ToListAsync();
        }

        public async Task<IEnumerable<Observation>> MyObservationsList(ApplicationUser user)
        {
            var observations = _dbContext.Observations
                    .Where(u => u.ApplicationUser == user)
                    .Include(b => b.Bird)
                    .OrderByDescending(d => d.ObservationDateTime)
                    .AsNoTracking()
                    .ToListAsync();
            return await observations;
        }

        public async Task<Observation> GetObservationDetails(int? id)
        {
            return await _dbContext.Observations
                .Include(b => b.Bird)
                .SingleOrDefaultAsync(m => m.ObservationId == id);
        }
    }
}
