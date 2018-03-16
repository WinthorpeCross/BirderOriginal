using Birder2.Data;
using Birder2.Models;
using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<Bird> AllBirdsDropDownList()
        {
            return _dbContext.Birds.ToList();
        }

        public IQueryable<Bird> AllBirdsList()
        {
            return _dbContext.Birds
                     .OrderByDescending(v => v.EnglishName)
                        .AsNoTracking();
        }

        public IQueryable<Bird> AllBirdsList(int birdId)
        {
            return _dbContext.Birds.Where(b => b.BirdId == birdId)
                        .OrderByDescending(v => v.EnglishName)
                            .AsNoTracking();
        }

        public IQueryable<Bird> CommonBirdsList()
        {
            return _dbContext.Birds
                .Where(b => b.BirderStatus == BirderStatus.Common)
                    .OrderByDescending(v => v.EnglishName)
                        .AsNoTracking();
        }

        public async Task<Bird> GetBirdDetails(int? id)
        {
            return await _dbContext.Birds
                            .Include(bcs => bcs.BirdConserverationStatus)
                                .SingleOrDefaultAsync(m => m.BirdId == id);
        }
    }
}
