using Birder2.Extensions;
using Birder2.Services;
using Birder2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Birder2.ViewComponents
{
    public class TopObservationsViewComponent : ViewComponent
    {
        private readonly ISideBarRepository _sideBarRepository;
        private readonly IApplicationUserAccessor _userAccessor;
        private readonly IMachineClockDateTime _systemClock;
        private readonly ILogger _logger;

        public TopObservationsViewComponent(ISideBarRepository sideBarRepository,
                                                IApplicationUserAccessor userAccessor,
                                                    ILogger<TopObservationsViewComponent> logger,
                                                        IMachineClockDateTime systemClock)
        {
            _sideBarRepository = sideBarRepository;
            _userAccessor = userAccessor;
            _logger = logger;
            _systemClock = systemClock;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            _logger.LogInformation(LoggingEvents.GetItem, "TopObservationsViewComponent");
            try
            {
                TopObservationsComponentViewModel viewModel = new TopObservationsComponentViewModel()
                {
                    TopObservations = await _sideBarRepository.GetTopObservations(await _userAccessor.GetUser()),
                    TopMonthlyObservations = await _sideBarRepository.GetTopObservations(await _userAccessor.GetUser(), _systemClock.Today)
                };
                return View("Default", viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.GetItemNotFound, ex, "TopObservationsViewComponent error");
                TopObservationsComponentViewModel viewModel = new TopObservationsComponentViewModel();
                return View(viewModel);
            }
        }
    }
}
