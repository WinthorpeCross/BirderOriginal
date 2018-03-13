using Birder2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
            //public IEnumberable<> TopObsersations { get; set; }
            //public IEnumberable<> TopMonthlyObsersations { get; set; }
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //var bir = await _sideBarRepository.TotalObservationsCount();
            return View();
        }
    }
}
