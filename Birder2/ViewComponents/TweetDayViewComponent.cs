﻿using Birder2.Extensions;
using Birder2.Services;
using Birder2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Birder2.ViewComponents
{
    //Tweet of the Day
    public class TweetDayViewComponent : ViewComponent
    {
        private readonly ISideBarRepository _sideBarRepository;
        private readonly IApplicationUserAccessor _userAccessor;
        private readonly ILogger _logger;
        private readonly IMachineClockDateTime _systemClock;

        public TweetDayViewComponent(ISideBarRepository sideBarRepository,
                                        IApplicationUserAccessor userAccessor,
                                            ILogger<TweetDayViewComponent> logger,
                                                IMachineClockDateTime systemClock)
        {
            _sideBarRepository = sideBarRepository;
            _userAccessor = userAccessor;
            _logger = logger;
            _systemClock = systemClock;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            _logger.LogInformation(LoggingEvents.GetItem, "TweetDayViewComponent");
            try
            {
                TweetDayViewModel viewModel = new TweetDayViewModel
                {
                    TweetOfTheDay = await _sideBarRepository.GetTweetOfTheDayAsync(_systemClock.Today)
                };
                if(viewModel.TweetOfTheDay == null)
                {
                    _logger.LogWarning(LoggingEvents.GetItemNotFound, "Tweet of the Day is returned as NULL");
                }
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.GetItemNotFound, ex, "TweetDayViewComponent error");
                TweetDayViewModel viewModel = new TweetDayViewModel();
                return View(viewModel);
            }
        }
    }
}
