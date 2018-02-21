using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Birder2.ViewComponents
{
    public class TopObservationsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //var bir = await _analysisRepository.BirdCount();
            return View();
        }
    }
}
