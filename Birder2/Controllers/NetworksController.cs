using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Birder2.Data;
using Birder2.Services;

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

        /*
         * Unfollow action
         * Find followers
         * Follow action
         * */


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
