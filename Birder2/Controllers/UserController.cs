using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Birder2.Models;
using Birder2.Services;
using Microsoft.Extensions.Logging;
using Birder2.ViewModels;
using Birder2.Extensions;
using System;

namespace Birder2.Controllers
{
    public class UserController : Controller
    {
        private const int pageSize = 10;
        private readonly IApplicationUserAccessor _userAccessor;
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;

        public UserController(IApplicationUserAccessor userAccessor,
                                IUserRepository userRepository,
                                    ILogger<Network> logger)
        {
            _userRepository = userRepository;
            _userAccessor = userAccessor;
            _logger = logger;
        }

        // GET: Network/Show/Winthorpe
        public async Task<IActionResult> Details(string userName, int page)
        {
            if (userName == null)
            {
                return NotFound();
            }

            if (page == 0)
            {
                page = 1;
            }

            var loggedinUser = await _userRepository.GetUserAndNetworkAsyncByUserName(await _userAccessor.GetUser());
            var userToShow = await _userRepository.GetUserAndNetworkAsyncByUserName(userName);

            //var followingList = loggedinUser.Following.ToList();
            var viewModel = new UserDetailsViewModel();

            if (loggedinUser != userToShow)
            {
                viewModel.IsFollowing = loggedinUser.Following.Any(cus => cus.ApplicationUser.UserName == userToShow.UserName);
            }
            viewModel.UserName = userToShow.UserName;
            viewModel.UniqueSpeciesCount = await _userRepository.UniqueSpeciesCount(userToShow);
            viewModel.Observations = await _userRepository.GetUsersObservationsList(userToShow.Id).GetPaged(page, pageSize);

            return View(viewModel);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //ToDo : Add validation
        public async Task<IActionResult> Follow(string userName, int currentPage)
        {

            _logger.LogInformation(LoggingEvents.UpdateItem, "Follow action called");
            try
            {
                var loggedinUser = await _userRepository.GetUserAndNetworkAsyncByUserName(await _userAccessor.GetUser());
                var userToFollow = await _userRepository.GetUserAndNetworkAsyncByUserName(userName);

                if (loggedinUser == userToFollow)
                {
                    return RedirectToAction("Details", new { userName = userName, page = currentPage });
                    //return BadRequest ???
                }
                else
                {
                    _userRepository.Follow(loggedinUser, userToFollow);
                    return RedirectToAction("Details", new { userName = userName, page = currentPage });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.GetItemNotFound, ex, "Follow action error");
                return RedirectToAction("Details", new { userName = userName, page = currentPage });
            }
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //ToDo : Add validation
        public async Task<IActionResult> Unfollow(string userName, int currentPage)
        {
            _logger.LogInformation(LoggingEvents.UpdateItem, "Unfollow action called");
            try
            {
                var loggedinUser = await _userRepository.GetUserAndNetworkAsyncByUserName(await _userAccessor.GetUser());
                var userToUnfollow = await _userRepository.GetUserAndNetworkAsyncByUserName(userName);

                if (loggedinUser == userToUnfollow)
                {
                    return RedirectToAction("Details", new { userName = userName, page = currentPage });
                }
                else
                {
                    _userRepository.UnFollow(loggedinUser, userToUnfollow);
                    return RedirectToAction("Details", new { userName = userName, page = currentPage });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.GetItemNotFound, ex, "Unfollow action error");
                return RedirectToAction("Details", new { userName = userName, page = currentPage });
            }
        }





        //private bool ApplicationUserExists(string id)
        //{
        //    return _context.ApplicationUser.Any(e => e.Id == id);
        //}
    }
}
