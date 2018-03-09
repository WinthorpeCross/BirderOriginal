using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Birder2.Data;
using Birder2.Services;
using Birder2.Models;
using Newtonsoft.Json;
using Birder2.ViewModels;

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

        public NetworksController(ApplicationDbContext context, IApplicationUserAccessor userAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
        }

        [HttpGet]
        public IActionResult Create(string searchCriterion)
        {
            FollowUserViewModel followUserViewModel = new FollowUserViewModel();

            if (string.IsNullOrEmpty(searchCriterion))
            {
                // ToDo: Suggested Birders to follow...
                // --> check for users that follow me but i do not follow...
                followUserViewModel.SearchResults = _context.Users.Include(x => x.Followers).Include(y => y.Following).ToList();
                followUserViewModel.SearchCriterion = searchCriterion;
            }
            else
            {
                followUserViewModel.SearchResults = _context.Users.Include(x => x.Followers).Include(y => y.Following).Where(un => un.NormalizedUserName.Contains(searchCriterion.ToUpper())).ToList();
                followUserViewModel.SearchCriterion = searchCriterion;
            }
            return View(followUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Follow([FromBody]ApplicationUser viewModel)
        {
            var user = await _userAccessor.GetUser();
            var loggedinUser = await _context.Users.Include(x => x.Followers).Include(y => y.Following).FirstOrDefaultAsync(x => x.Id == user.Id);

            var userToFollow = await _context.Users.Include(x => x.Followers).Include(y => y.Following).FirstOrDefaultAsync(x => x.Id == viewModel.Id);


            if (loggedinUser == userToFollow)
            {
                return Json(JsonConvert.SerializeObject("An error occured"));
            }

            userToFollow.Followers.Add(new Network
            {
                Follower = loggedinUser //Follower  <-- Independent
            });

            _context.SaveChanges();


            return Json(JsonConvert.SerializeObject(viewModel));

        }

        [HttpPost]
        public async Task<IActionResult> Follow2([FromBody]Network viewModel)
        {
            var user = await _userAccessor.GetUser();
            var loggedinUser = await _context.Users.Include(x => x.Followers).Include(y => y.Following).FirstOrDefaultAsync(x => x.Id == user.Id);

            var userToFollow = await _context.Users.Include(x => x.Followers).Include(y => y.Following).FirstOrDefaultAsync(x => x.Id == viewModel.ApplicationUser.Id);

            if (loggedinUser == userToFollow)
            {
                return Json(JsonConvert.SerializeObject("An error occured"));
            }

            userToFollow.Followers.Add(new Network
            {
                Follower = loggedinUser //Follower  <-- Independent
            });

            _context.SaveChanges();

            return Json(JsonConvert.SerializeObject(viewModel));
        }

        [HttpPost]
        public async Task<JsonResult> UnFollow2([FromBody]ApplicationUser viewModel)
        {
            var user = await _userAccessor.GetUser();
            var loggedinUser = await _context.Users.Include(x => x.Followers).Include(y => y.Following).FirstOrDefaultAsync(x => x.Id == user.Id);

            var userToUnfollow = await _context.Users.Include(x => x.Followers).Include(y => y.Following).FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            if (loggedinUser == userToUnfollow)
            {
                return Json(JsonConvert.SerializeObject("An error occured"));
            }

            loggedinUser.Following.Remove(userToUnfollow.Followers.FirstOrDefault());

            _context.SaveChanges();


            return Json(JsonConvert.SerializeObject(viewModel));


        }

        [HttpPost]
        public async Task<JsonResult> UnFollow([FromBody]Network viewModel)
        {
            var user = await _userAccessor.GetUser();
            var loggedinUser = await _context.Users.Include(x => x.Followers).Include(y => y.Following).FirstOrDefaultAsync(x => x.Id == user.Id);

            var userToUnfollow = await _context.Users.Include(x => x.Followers).Include(y => y.Following).FirstOrDefaultAsync(x => x.Id == viewModel.ApplicationUser.Id);

            if (loggedinUser == userToUnfollow)
            {
                return Json(JsonConvert.SerializeObject("An error occured"));
            }

            loggedinUser.Following.Remove(userToUnfollow.Followers.FirstOrDefault());

            _context.SaveChanges();


            return Json(JsonConvert.SerializeObject(viewModel));


        }

        // GET: Networks
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
            //var applicationDbContext = _context.Network.Include(n => n.ApplicationUser).Include(n => n.Follower);

            return View(loggedinUser);
            //return Ok(loggedinUser);
        }
    }
}

//loggedinUser.Following.Add(loggedinUser.Followers.FirstOrDefault());
//userX.Following.Remove(userZ.Followers.FirstOrDefault());

