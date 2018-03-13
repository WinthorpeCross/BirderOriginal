using Birder2.Services;
using Birder2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Birder2.ViewComponents
{
    public class TopObservationsViewComponent : ViewComponent
    {
        private readonly ISideBarRepository _sideBarRepository;
        private readonly IApplicationUserAccessor _userAccessor;
        private readonly ILogger _logger;

        public TopObservationsViewComponent(ISideBarRepository sideBarRepository,
                                                IApplicationUserAccessor userAccessor,
                                                    ILogger<TopObservationsViewComponent> logger)
        {
            _sideBarRepository = sideBarRepository;
            _userAccessor = userAccessor;
            _logger = logger;
        }

        public class TopObservationsViewModel
        {
            public IEnumerable<LifeListViewModel> TopObsersations { get; set; }
            //public IEnumberable<> TopMonthlyObsersations { get; set; }
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            TopObservationsViewModel tovm = new TopObservationsViewModel()
            {
                TopObsersations = await _sideBarRepository.GetLifeList(await _userAccessor.GetUser())
            };
             
            //var bir = await _sideBarRepository.TotalObservationsCount();
            return View("Default", tovm);
        }
    }
}
