using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Birder2.Services;
using Microsoft.AspNetCore.Authorization;
using Birder2.ViewModels;
using FlickrNet;

namespace Birder2.Controllers
{
    [Authorize]
    public class BirdController : Controller
    {
        private readonly IBirdRepository _birdRepository;

        public BirdController(IBirdRepository birdRepository)
        {
            _birdRepository = birdRepository;
        }

        // GET: Bird
        public async Task<IActionResult> Index(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                return View(await _birdRepository.FilteredBirdsList(searchString));
            }
            else
            {
                return View(await _birdRepository.AllBirdsList());
            }
        }

        // GET: Bird/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // ****************************************

            string myFlickrApiKey = "7700051a31f80a964a5d0037ad5ed564";
            string myFlickrSecret = "59f50feafa488bad";
            //string query = "Cyanistes caeruleus";

            Flickr flickr = new Flickr(myFlickrApiKey, myFlickrSecret);

            //var options = new PhotoSearchOptions { Text = query, Extras = PhotoSearchExtras.AllUrls, SafeSearch = SafetyLevel.Safe, MediaType = MediaType.Photos };

            // ****************************************

            var model = new BirdDetailViewModel()
            {
                Bird = await _birdRepository.GetBirdDetails(id),
                //BirdPhotos = flickr.PhotosSearch(options) ---> set species in SERVICE
            };

            //string query = model.Bird.Species;
            var options = new PhotoSearchOptions { Text = model.Bird.Species, Extras = PhotoSearchExtras.AllUrls, SafeSearch = SafetyLevel.Safe, MediaType = MediaType.Photos };
            model.BirdPhotos = flickr.PhotosSearch(options);


            //var bird = await _birdRepository.GetBirdDetails(id);

            if (model.Bird == null)
            {
                return NotFound();
            }
            return View(model);
        }
    }
}