using Birder2.Data;
using Birder2.Models;
using Birder2.Services;
using FlickrNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;

namespace Birder2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IFlickrService _flickrService;
        private readonly ApplicationDbContext _context;
        private readonly IApplicationUserAccessor _userAccessor;

        public HomeController(ApplicationDbContext context,
                                IFlickrService flickrService,
                                    IApplicationUserAccessor userAccessor)
        {
            _context = context;
            _flickrService = flickrService;
            _userAccessor = userAccessor;
            //blobUtility = new HomeController.BlobUtility(_optionsAccessor.Value.StorageAccountNameOption, _optionsAccessor.Value.StorageAccountKeyOption);
        }

        //public BlobUtility(string accountName, string accountKey)
        //{
        //    CloudStorageAccount storageAccount = 
        //}

        public IActionResult Index() //async Task<>
        {
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
