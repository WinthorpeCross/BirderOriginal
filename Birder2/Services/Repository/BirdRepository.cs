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

        public async Task<IEnumerable<Bird>> AllBirdsList(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                return await _dbContext.Birds.Where(h => h.EnglishName.ToUpper().Contains(searchString.ToUpper())).ToListAsync();
            }
            else
            {
                return await _dbContext.Birds.ToListAsync();
            }
        }

        public async Task<IEnumerable<Bird>> CommonBirdsList(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                return await _dbContext.Birds
                    .Include(bs => bs.BritishStatus)
                    .Where(bs => bs.BritishStatus.BirderStatusInBritain == "Common" && bs.EnglishName.ToUpper().Contains(searchString.ToUpper()))
                    .ToListAsync();
            }
            else
            {
                return await _dbContext.Birds
                    .Include(bs => bs.BritishStatus)
                    .Where(bs => bs.BritishStatus.BirderStatusInBritain == "Common")
                    .ToListAsync();
            }
        }

        public async Task<Bird> GetBirdDetails(int? id)
        {
            return await _dbContext.Birds
                .Include(bcs => bcs.BirdConserverationStatus)
                .Include(bs => bs.BritishStatus)
                .SingleOrDefaultAsync(m => m.BirdId == id);
                     //.Include(o => o.Observations)
                     //.ThenInclude(au => au.ApplicationUser)
        }
    }
}
