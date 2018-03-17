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
        private readonly IObservationRepository _observationRepository;
        private readonly IApplicationUserAccessor _userAccessor;
        private readonly ILogger _logger;

        public BirdCountViewComponent(IObservationRepository observationRepository,
                                            IApplicationUserAccessor userAccessor,
                                                ILogger<BirdCountViewComponent> logger)
        {
            _observationRepository = observationRepository;
            _userAccessor = userAccessor;
            _logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            _logger.LogInformation(LoggingEvents.GetItem, "BirdCountViewComponent");
            try
            {
                BirdCountViewModel viewModel = new BirdCountViewModel()
                {
                    TotalObservations = await _observationRepository.TotalObservationsCount(await _userAccessor.GetUser()),
                    TotalSpecies = await _observationRepository.UniqueSpeciesCount(await _userAccessor.GetUser())
                };
                return View("Default", viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.GetItemNotFound, ex, "BirdCountViewComponent error");
                BirdCountViewModel viewModel = new BirdCountViewModel();
                return View("Default", viewModel);
            }
        }
    }
}
