using Birder2.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Birder2.Controllers
{
    public class ListController : Controller
    {
        private readonly ILIstService _listService;
        private readonly IApplicationUserAccessor _userAccessor;
        private readonly IObservationsAnalysisService _observationsAnalysisService;

        public ListController(ILIstService listService,
                                IApplicationUserAccessor userAccessor,
                                    IObservationsAnalysisService observationsAnalysisService)
        {
            _listService = listService;
            _userAccessor = userAccessor;
            _observationsAnalysisService = observationsAnalysisService;
        }

        [HttpGet]
        public async Task<IActionResult> ListLife(string userName)
        {
            // ToDo: Refactor so one can get another user's Life List
            var user = await _userAccessor.GetUser();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if(string.IsNullOrEmpty(userName))
            {
                userName = user.UserName;
            }

            var viewModel = _listService.GetLifeList(userName);

            return View(viewModel);
        }
    }
}
