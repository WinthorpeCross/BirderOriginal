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

        public async Task<IActionResult> Index()
        {
            var user = await _userAccessor.GetUser();
            var t = _context.Users.Include(x => x.Followers).Include(y => y.Following).FirstOrDefault(x => x.Id == user.Id);

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
