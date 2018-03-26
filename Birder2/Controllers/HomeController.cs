using Birder2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Birder2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public HomeController()
        {
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
            //PhotoCollection photos = _flickrService.GetFlickrPhotoCollection("Cyanistes caeruleus");
            return View();
        }
 
        [AllowAnonymous]
        public IActionResult Contact()
        {
            //ViewData["Message"] = "Map example";
            return View();
        }

        [AllowAnonymous]
        public IActionResult Welcome()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
