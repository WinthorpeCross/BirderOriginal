﻿using Birder2.Data;
using Birder2.Services;
using Birder2.Models;
using Birder2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Birder2.Extensions;
using System;

/*
<script>
    document.getElementById("ItemPreview").src = "data:image/png;base64, @UserManager.GetUserAsync(User).Result.UserPhoto";
</script>
*/

namespace Birder2.Controllers
{
    public class NetworksController : Controller
    {
        private readonly ApplicationDbContext _context; // <------------- delete
        private readonly IApplicationUserAccessor _userAccessor;
        private readonly ILogger _logger;
        private readonly IUserRepository _userRepository;

        public NetworksController(ApplicationDbContext context // <------------- delete
                                       , IApplicationUserAccessor userAccessor
                                            ,ILogger<Network> logger
                                                ,IUserRepository userRepository)
        {
            _context = context;
            _userAccessor = userAccessor;
            _logger = logger;
            _userRepository = userRepository;
        }

        // GET: Networks
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation(LoggingEvents.ListItems, "Network Index called");
            try
            {
                ApplicationUser loggedinUser = await _userRepository.GetUserAndNetworkAsyncByUserName(await _userAccessor.GetUser());
                NetworkIndexViewModel viewModel = new NetworkIndexViewModel();

                viewModel.FollowingList = await _userRepository.GetFollowingList(loggedinUser);
                viewModel.FollowersList = await _userRepository.GetFollowersList(loggedinUser);

                return View(viewModel);
            }
            catch (Exception ex)
            {
                // ToDo: What to log?  What to return?  Generic error page?
                _logger.LogError(LoggingEvents.GetItemNotFound, ex, "Network Index() error");
                //_logger.LogError($"failed to return Birds details page: {ex}");//  <--
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create(string searchCriterion)
        {
            _logger.LogInformation(LoggingEvents.ListItems, "Nework Index called");
            try
            {
                ApplicationUser loggedinUser = await _userRepository.GetUserAndNetworkAsyncByUserName(await _userAccessor.GetUser());
                FollowUserViewModel followUserViewModel = new FollowUserViewModel();

                if (String.IsNullOrEmpty(searchCriterion))
                {
                    followUserViewModel.SearchResults = await _userRepository.GetSuggestedBirdersToFollow(loggedinUser);
                    followUserViewModel.SearchCriterion = searchCriterion;
                }
                else
                {
                    followUserViewModel.SearchResults = await _userRepository.GetSuggestedBirdersToFollow(loggedinUser, searchCriterion);
                    followUserViewModel.SearchCriterion = searchCriterion;
                }
                return View(followUserViewModel);
            }
            catch (Exception ex)
            {
                // ToDo: What to log?  What to return?  Generic error page?
                _logger.LogError(LoggingEvents.GetItemNotFound, ex, "Network Index() error");
                //_logger.LogError($"failed to return Birds details page: {ex}");//  <--
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Follow([FromBody]UserViewModel viewModel)
        {
            _logger.LogInformation(LoggingEvents.UpdateItem, "Follow action called");
            try
            {
                ApplicationUser loggedinUser = await _userRepository.GetUserAndNetworkAsyncByUserName(await _userAccessor.GetUser());
                ApplicationUser userToFollow = await _userRepository.GetUserAndNetworkAsyncByUserName(viewModel.UserName);

                if (loggedinUser == userToFollow)
                {
                    return Json(JsonConvert.SerializeObject("An error occured"));
                    //return BadRequest ???
                }
                else
                {
                    _userRepository.Follow(loggedinUser, userToFollow);
                    return Json(JsonConvert.SerializeObject(viewModel));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.GetItemNotFound, ex, "Follow action error");
                return Json(JsonConvert.SerializeObject("An error occured"));
            }
        }

        [HttpPost]
        public async Task<JsonResult> UnFollow([FromBody]UserViewModel viewModel)
        {
            _logger.LogInformation(LoggingEvents.UpdateItem, "Follow action called");
            try
            {
                ApplicationUser loggedinUser = await _userRepository.GetUserAndNetworkAsyncByUserName(await _userAccessor.GetUser());
                ApplicationUser userToUnfollow = await _userRepository.GetUserAndNetworkAsyncByUserName(viewModel.UserName);

                if (loggedinUser == userToUnfollow)
                {
                    return Json(JsonConvert.SerializeObject("An error occured"));
                }
                else
                {
                    _userRepository.UnFollow(loggedinUser, userToUnfollow);
                    return Json(JsonConvert.SerializeObject(viewModel));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.GetItemNotFound, ex, "Follow action error");
                return Json(JsonConvert.SerializeObject("An error occured"));
            }
        }         
    }
}


