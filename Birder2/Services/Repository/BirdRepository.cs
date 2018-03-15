using Birder2.Data;
using Birder2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Birder2.Services
{
    public class BirdRepository : IBirdRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public BirdRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Bird>> AllBirdsList()
        {
            return await _dbContext.Birds.ToListAsync();
        }

        public async Task<IEnumerable<Bird>> AllBirdsList(int birdId)
        {
            return await _dbContext.Birds.Where(b => b.BirdId == birdId).ToListAsync();
        }

        public async Task<IEnumerable<Bird>> CommonBirdsList()
        {
                return await _dbContext.Birds
                    //.Include(bs => bs.BirderStatus)
                    .Where(b => b.BirderStatus == BirderStatus.Common)
                    .ToListAsync();
        }

        public async Task<Bird> GetBirdDetails(int? id)
        {
            return await _dbContext.Birds
                .Include(bcs => bcs.BirdConserverationStatus)
                //.Include(bs => bs.BirderStatus)
                .SingleOrDefaultAsync(m => m.BirdId == id);
                     //.Include(o => o.Observations)
                     //.ThenInclude(au => au.ApplicationUser)
        }
    }
}
