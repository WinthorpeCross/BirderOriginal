using Birder2.Data;
using Birder2.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Birder2.Services
{
    public interface ILIstService
    {
        LifeListViewModel GetLifeList(string userId);
    }

    public class ListService : ILIstService
    {
        private readonly ApplicationDbContext _dbContext;

        public ListService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public LifeListViewModel GetLifeList(string userId)
        {
            LifeListViewModel viewModel = new LifeListViewModel();

            viewModel.TotalObservations = (from observations in _dbContext.Observations
                                               where (observations.ApplicationUserId == userId)
                                                select observations).Count();

            viewModel.TotalSpecies = (from observations in _dbContext.Observations
                                           where (observations.ApplicationUserId == userId)
                                           select observations.BirdId).Distinct().Count();

            viewModel.LifeList = (from observations in _dbContext.Observations
                 .Include(b => b.Bird)
                    .ThenInclude(u => u.BirdConserverationStatus)
                 .Where(u => u.ApplicationUser.Id == userId)
                    group observations by observations.Bird into species
                    orderby species.Count() descending
                    select new SpeciesSummaryViewModel
                    {
                        Vernacular = species.FirstOrDefault().Bird.EnglishName,
                        ScientificName = species.FirstOrDefault().Bird.Species,
                        PopSize = species.FirstOrDefault().Bird.PopulationSize,
                        BtoStatus = species.FirstOrDefault().Bird.BtoStatusInBritain,
                        ConservationStatus = species.FirstOrDefault().Bird.BirdConserverationStatus.ConservationStatus,
                        Count = species.Count()
                    });

            return viewModel;
        }
    }
}
