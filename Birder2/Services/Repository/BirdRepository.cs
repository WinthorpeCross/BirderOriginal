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

        public async Task<IEnumerable<Bird>> FilteredBirdsList(string searchString)
        {
            return await _dbContext.Birds.Where(h => h.EnglishName.ToUpper().Contains(searchString.ToUpper())).ToListAsync();
        }

        public async Task<Bird> GetBirdDetails(int? id)
        {
            return await _dbContext.Birds.SingleOrDefaultAsync(m => m.BirdId == id);
        }
    }
}
