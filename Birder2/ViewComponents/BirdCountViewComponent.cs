using Birder2.Extensions;
using Birder2.Services;
using Birder2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Birder2.ViewComponents
{
    public class BirdCountViewComponent : ViewComponent
    {
        private readonly IApplicationUserAccessor _userAccessor;
        private readonly ILogger _logger;
        private readonly IObservationsAnalysisService _observationsAnalysisService;

        public BirdCountViewComponent(IApplicationUserAccessor userAccessor,
                                        IObservationsAnalysisService observationsAnalysisService,
                                            ILogger<BirdCountViewComponent> logger)
        {
            _logger = logger;
            _userAccessor = userAccessor;
            _observationsAnalysisService = observationsAnalysisService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            _logger.LogInformation(LoggingEvents.GetItem, "BirdCountViewComponent");
            try
            {
                var user = await _userAccessor.GetUser();
                ObservationsAnalysisDto viewModel = await _observationsAnalysisService.GetObservationAnalysis(user);
                
                //BirdCountViewModel viewModel = new BirdCountViewModel()
                //{
                //    TotalObservations = await _observationRepository.TotalObservationsCount(await _userAccessor.GetUser()),
                //    TotalSpecies = await _observationRepository.UniqueSpeciesCount(await _userAccessor.GetUser())
                //};
                return View("Default", viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.GetItemNotFound, ex, "BirdCountViewComponent error");
                ObservationsAnalysisDto viewModel = new ObservationsAnalysisDto();
                return View("Default", viewModel);
            }
        }
    }
}
