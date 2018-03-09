using Birder2.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Birder2.ViewComponents
{
    public class TopObservationsViewComponent : ViewComponent
    {
        private readonly IAnalysisRepository _analysisRepository;

        public TopObservationsViewComponent(IAnalysisRepository analysisRepository)
        {
            _analysisRepository = analysisRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var bir = await _analysisRepository.BirdCount();
            return View();
        }
    }
}
