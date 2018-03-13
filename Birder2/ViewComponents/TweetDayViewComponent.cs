using Birder2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Birder2.ViewComponents
{
    //Tweet of the Day
    public class TweetDayViewComponent : ViewComponent
    {
        private readonly ISideBarRepository _sideBarRepository;
        private readonly IApplicationUserAccessor _userAccessor;
        private readonly ILogger _logger;

        public TweetDayViewComponent(ISideBarRepository sideBarRepository,
                                            IApplicationUserAccessor userAccessor,
                                                ILogger<TweetDayViewComponent> logger)
        {
            _sideBarRepository = sideBarRepository;
            _userAccessor = userAccessor;
            _logger = logger;
        }

        public class TweetDayViewModel
        {

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //var bir = await _analysisRepository.BirdCount();
            TweetDayViewModel viewModel = new TweetDayViewModel();
            return View();
        }
    }
}
