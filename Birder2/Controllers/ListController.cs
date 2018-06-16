using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Birder2.Data;
using Birder2.Models;
using Birder2.Services;
using Birder2.ViewModels;

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
            //{
            //    ObservationsAnalysisDto = await _observationsAnalysisService.GetObservationAnalysis(user),
            //    LifeList = _listService.GetLifeList(user.Id)
            //};

            return View(viewModel);
        }
    }
}
