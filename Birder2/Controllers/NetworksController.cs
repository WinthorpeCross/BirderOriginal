using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Birder2.Data;
using Birder2.Services;
using Birder2.Models;
using Newtonsoft.Json;
using Birder2.ViewModels;
using Microsoft.Extensions.Logging;

/*
<script>
    document.getElementById("ItemPreview").src = "data:image/png;base64, @UserManager.GetUserAsync(User).Result.UserPhoto";
</script>
*/

namespace Birder2.Controllers
{
    public class NetworksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IApplicationUserAccessor _userAccessor;
        private readonly ILogger _logger;

        public NetworksController(ApplicationDbContext context
                                       ,IApplicationUserAccessor userAccessor
                                            ,ILogger<Network> logger)
        {
            _context = context;
            _userAccessor = userAccessor;
            _logger = logger;
        }

        // GET: Networks
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userAccessor.GetUser();
            var loggedinUser = await _context.Users
                .Include(x => x.Followers)
                    .ThenInclude(x => x.Follower)
                .Include(y => y.Following)
                    .ThenInclude(r => r.ApplicationUser)
                .Where(x => x.Id == user.Id)
                .FirstOrDefaultAsync();

            NetworkIndexViewModel viewModel = new NetworkIndexViewModel();

            viewModel.FollowingList =
                                 from following in loggedinUser.Following
                                 select new UserViewModel
                                 {
                                     UserName = following.ApplicationUser.UserName,
                                     ProfileImage = following.ApplicationUser.ProfileImage
                                 };

            viewModel.FollowersList =
                                from follower in loggedinUser.Followers
                                select new UserViewModel
                                {
                                    UserName = follower.Follower.UserName,
                                    ProfileImage = follower.Follower.ProfileImage
                                };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create(string searchCriterion)
        {
            var user = await _userAccessor.GetUser();
            FollowUserViewModel followUserViewModel = new FollowUserViewModel();

            if (string.IsNullOrEmpty(searchCriterion))
            {
                // ToDo: Suggested Birders to follow...
                // --> check for users that follow me but i do not follow...
                followUserViewModel.SearchResults = from users in _context.Users
                                                    where(users.UserName != user.UserName)
                                                    select new UserViewModel
                                                    {
                                                        UserName = users.UserName,
                                                        ProfileImage = users.ProfileImage
                                                    };
                followUserViewModel.SearchCriterion = searchCriterion;
            }
            else
            {
                followUserViewModel.SearchResults = from users in _context.Users
                                                    where(users.UserName.ToUpper().Contains(searchCriterion.ToUpper()) && users.UserName != user.UserName)
                                                    select new UserViewModel
                                                    {
                                                        UserName = users.UserName,
                                                        ProfileImage = users.ProfileImage
                                                    };

                followUserViewModel.SearchCriterion = searchCriterion;
            }
            return View(followUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Follow([FromBody]UserViewModel viewModel)
        {
            var user = await _userAccessor.GetUser();
            var loggedinUser = await _context.Users.Include(x => x.Followers)
                                                        .Include(y => y.Following)
                                                            .FirstOrDefaultAsync(x => x.Id == user.Id);

            var userToFollow = await _context.Users.Include(x => x.Followers)
                                                        .Include(y => y.Following)
                                                            .FirstOrDefaultAsync(x => x.UserName == viewModel.UserName);

            if (loggedinUser == userToFollow)
            {
                return Json(JsonConvert.SerializeObject("An error occured"));
                //return BadRequest ???
            }

            userToFollow.Followers.Add(new Network
            {
                Follower = loggedinUser //Follower  <-- Independent
            });

            _context.SaveChanges();

            return Json(JsonConvert.SerializeObject(viewModel));
        }

        [HttpPost]
        public async Task<JsonResult> UnFollow([FromBody]UserViewModel viewModel)
        {
            var user = await _userAccessor.GetUser();
            var loggedinUser = await _context.Users.Include(x => x.Followers)
                                                        .Include(y => y.Following)
                                                            .FirstOrDefaultAsync(x => x.Id == user.Id);

            var userToUnfollow = await _context.Users.Include(x => x.Followers)
                                                        .Include(y => y.Following)
                                                            .FirstOrDefaultAsync(x => x.UserName == viewModel.UserName);

            if (loggedinUser == userToUnfollow)
            {
                return Json(JsonConvert.SerializeObject("An error occured"));
            }

            loggedinUser.Following.Remove(userToUnfollow.Followers.FirstOrDefault());

            _context.SaveChanges();

            return Json(JsonConvert.SerializeObject(viewModel));
        }         
    }
}

//var applicationDbContext = _context.Network.Include(n => n.ApplicationUser).Include(n => n.Follower);
//loggedinUser.Following.Add(loggedinUser.Followers.FirstOrDefault());
//userX.Following.Remove(userZ.Followers.FirstOrDefault());

