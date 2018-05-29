using Birder2.Extensions;
using Birder2.Models;
using Birder2.Services;
using Birder2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Birder2.Controllers
{
    public class NetworkController : Controller
    {
        private readonly IApplicationUserAccessor _userAccessor;
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;

        public NetworkController(IApplicationUserAccessor userAccessor
                                        ,ILogger<Network> logger
                                            ,IUserRepository userRepository)
        {
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

                viewModel.FollowingList = _userRepository.GetFollowingList(loggedinUser);
                viewModel.FollowersList = _userRepository.GetFollowersList(loggedinUser);

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
                var followUserViewModel = new FollowUserViewModel();

                if (String.IsNullOrEmpty(searchCriterion))
                {
                    followUserViewModel.SearchResults = _userRepository.GetSuggestedBirdersToFollow(loggedinUser);
                    followUserViewModel.SearchCriterion = searchCriterion;
                }
                else
                {
                    followUserViewModel.SearchResults = _userRepository.GetSuggestedBirdersToFollow(loggedinUser, searchCriterion);
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


