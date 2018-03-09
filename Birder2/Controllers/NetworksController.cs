using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Birder2.Data;
using Birder2.Services;
using Birder2.Models;
using Newtonsoft.Json;

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

        public class FollowUserViewModel
        {
            public string SearchCriterion { get; set; }
            public string StatusMessage { get; set; }
            private IEnumerable<ApplicationUser> _searchResults;
            public IEnumerable<ApplicationUser> SearchResults
            {
                get
                {
                    return _searchResults ?? (_searchResults = new List<ApplicationUser>());
                }
                set
                {
                    _searchResults = value;
                }
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            FollowUserViewModel followUserViewModel = new FollowUserViewModel();

            followUserViewModel.SearchResults = _context.Users.Include(x => x.Followers).Include(y => y.Following).ToList();

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

/*

var user = await _userAccessor.GetUser();
var loggedinUser = await _context.Users.Include(x => x.Followers).Include(y => y.Following).FirstOrDefaultAsync(x => x.Id == user.Id);
var userX = await _context.Users.Include(x => x.Followers).Include(y => y.Following).FirstOrDefaultAsync(x => x.UserName == "Winthorpe");
var userY = await _context.Users.Include(x => x.Followers).Include(y => y.Following).FirstOrDefaultAsync(x => x.UserName == "Tenko");
var userZ = await _context.Users.Include(x => x.Followers).Include(y => y.Following).FirstOrDefaultAsync(x => x.UserName == "Max");

var a = 1;

//I follow or unfollow someone else.  That's the only action
// X follows Y
//Followed  <-- Dependent
//userZ.Followers.Add(new Network
//{
//    Follower = userY //Follower  <-- Independent
//});
// X unfollows Y
loggedinUser.Following.Add(loggedinUser.Followers.FirstOrDefault());
//userX.Following.Remove(userZ.Followers.FirstOrDefault());
//groupToUpdate.GruposUsuarios.Remove(groupToUpdate.GruposUsuarios.Where(ugu => ugu.UserId == userToUpdate.Id).FirstOrDefault());

//_context.SaveChanges();

userX = await _context.Users.Include(x => x.Followers).Include(y => y.Followers).FirstOrDefaultAsync(x => x.UserName == "Winthorpe");
userY = await _context.Users.Include(x => x.Followers).Include(y => y.Following).FirstOrDefaultAsync(x => x.UserName == "Tenko");
userZ = await _context.Users.Include(x => x.Followers).Include(y => y.Following).FirstOrDefaultAsync(x => x.UserName == "Max");
var f = userX.Following.ToList();
//var follower
//var userToFollow

var b = 1;

*/
