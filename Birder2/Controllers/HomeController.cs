using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Birder2.Models;
using Microsoft.AspNetCore.Authorization;
using FlickrNet;

namespace Birder2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult About()
        {
            /*
             * Install-Package FlickrNet -Version 4.0.4-alpha  |  https://www.nuget.org/packages/FlickrNet/4.0.4-alpha
             * Above FlickerNet package for .NET Core.  Not available through NuGet
             * see https://github.com/samjudson/flickr-net for documentation
             * */
            string myFlickrApiKey = "7700051a31f80a964a5d0037ad5ed564";
            string myFlickrSecret = "59f50feafa488bad";
            string query = "Cyanistes caeruleus";

            //Flickr flickr = new Flickr(myApiKey);
            Flickr flickr = new Flickr(myFlickrApiKey, myFlickrSecret);

            //PhotoSearchOptions options = new PhotoSearchOptions();

            //options.SafeSearch = SafetyLevel.Safe;
            //options.Licenses.Add(LicenseType.AttributionCC);
            //options.MediaType = MediaType.Photos;
            var options = new PhotoSearchOptions { Text = query, Extras = PhotoSearchExtras.AllUrls, SafeSearch = SafetyLevel.Safe, MediaType = MediaType.Photos };
            //*Tags = "colorful",*/ PerPage = 20, Page = 1 };
            //options.Text = query;
            
            //options.Extras = PhotoSearchExtras.AllUrls;

            PhotoCollection photos = flickr.PhotosSearch(options);

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
