using Birder2.Data;
using Birder2.Models;
using Birder2.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Birder2.Services
{
    public class ObservationsAnalysisService : IObservationsAnalysisService
    {
        private readonly ApplicationDbContext _dbContext;

        public ObservationsAnalysisService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ObservationsAnalysisDto> GetObservationAnalysis(ApplicationUser user)
        {
            var viewModel = new ObservationsAnalysisDto();
            viewModel.TotalObservations = await (from observations in _dbContext.Observations
                          where (observations.ApplicationUserId == user.Id)
                          select observations).CountAsync();

            viewModel.TotalSpecies = await (from observations in _dbContext.Observations
                          where (observations.ApplicationUserId == user.Id)
                          select observations.BirdId).Distinct().CountAsync();
            return viewModel;
        }

    }
}
