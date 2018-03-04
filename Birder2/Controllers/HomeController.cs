using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Birder2.Models;
using Microsoft.AspNetCore.Authorization;
using FlickrNet;
using Birder2.Services;
using Birder2.Data;
using Microsoft.EntityFrameworkCore;

namespace Birder2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IFlickrService _flickrService;
        private readonly ApplicationDbContext _context;
        private readonly IApplicationUserAccessor _userAccessor;

        public HomeController(ApplicationDbContext context, IFlickrService flickrService, IApplicationUserAccessor userAccessor)
        {
            _context = context;
            _flickrService = flickrService;
            _userAccessor = userAccessor;
        }

        public class TestViewModel
        {
            public int FollowersCount { get; set; }
            public int FollowingCount { get; set; }

        }

        public async Task<IActionResult> Index()
        {
            var user = await _userAccessor.GetUser();
            var loggedinUser = await _context.Users.Include(x => x.Followers).Include(y => y.Following).FirstOrDefaultAsync(x => x.Id == user.Id);
            var userX = await _context.Users.Include(x => x.Followers).Include(y => y.Following).FirstOrDefaultAsync(x => x.UserName == "Winthorpe");
            var userY = await _context.Users.Include(x => x.Followers).Include(y => y.Following).FirstOrDefaultAsync(x => x.UserName == "Tenko");
            var userZ = await _context.Users.Include(x => x.Followers).Include(y => y.Following).FirstOrDefaultAsync(x => x.UserName == "Max");

            var a = 1;

            //I follow or unfollow someone else.  That's the only action
            // X follows Y
            //userZ.Followers.Add(new Network
            //{
            //    Follower = userX
            //});
            // X unfollows Y
            //userX.Following.Remove(userZ.Followers.FirstOrDefault());
            //groupToUpdate.GruposUsuarios.Remove(groupToUpdate.GruposUsuarios.Where(ugu => ugu.UserId == userToUpdate.Id).FirstOrDefault());

            //_context.SaveChanges();

            userX = await _context.Users.Include(x => x.Followers).Include(y => y.Following).FirstOrDefaultAsync(x => x.UserName == "Winthorpe");
            userY = await _context.Users.Include(x => x.Followers).Include(y => y.Following).FirstOrDefaultAsync(x => x.UserName == "Tenko");
            userZ = await _context.Users.Include(x => x.Followers).Include(y => y.Following).FirstOrDefaultAsync(x => x.UserName == "Max");
            var f = userX.Following.ToList();
            //var follower
            //var userToFollow

            var b = 1;


            var model = new TestViewModel()
            {
                //FollowersCount = t.Followers.Count(),
                //FollowingCount = t.Following.Count()
            };
            return View();
        }

        [AllowAnonymous]
        public IActionResult About()
        {
            PhotoCollection photos = _flickrService.GetFlickrPhotoCollection("Cyanistes caeruleus");
            return View(photos);
        }
 
        [AllowAnonymous]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Map example";
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
