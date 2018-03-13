using Birder2.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Birder2.ViewComponents
{
    public class TopObservationsViewComponent : ViewComponent
    {
        private readonly ISideBarRepository _sideBarRepository;

        public TopObservationsViewComponent(ISideBarRepository sideBarRepository)
        {
            _sideBarRepository = sideBarRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //var bir = await _sideBarRepository.TotalObservationsCount();
            return View();
        }
    }
}
