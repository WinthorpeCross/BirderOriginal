using Birder2.Data;
using Microsoft.EntityFrameworkCore;
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
