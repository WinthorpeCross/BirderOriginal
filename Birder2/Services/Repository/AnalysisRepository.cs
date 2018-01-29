using Birder2.Data;
using Birder2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Birder2.Services
{
    public class AnalysisRepository : IAnalysisRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AnalysisRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> BirdCount()
        {
            return await _dbContext.Birds.CountAsync();
        }   

    }
}
