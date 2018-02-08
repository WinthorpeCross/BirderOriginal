using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Birder2.Services;
using Microsoft.AspNetCore.Authorization;
using Birder2.ViewModels;

namespace Birder2.Controllers
{
    [Authorize]
    public class BirdController : Controller
    {
        private readonly IBirdRepository _birdRepository;
        private readonly IFlickrService _flickrService;

        public BirdController(IBirdRepository birdRepository, IFlickrService flickrService)
        {
            _birdRepository = birdRepository;
            _flickrService = flickrService;
        }

        // GET: All Bird Species
        public async Task<IActionResult> Index(string searchString, string searchType)
        {
            switch (searchType)
            {
                case "CommonBirds":
                    Console.WriteLine("Case 1");  //----> View Header title
                    ViewData["searchType"] = searchType;
                    ViewData["Title"] = "Common British Bird Species";
                    return View(await _birdRepository.CommonBirdsList(searchString));

                case "MyBirds":
                    Console.WriteLine("Case 2");  //----> View Header title
                    ViewData["searchType"] = searchType;
                    ViewData["Title"] = "My Observed British Bird Species";
                    return View(await _birdRepository.AllBirdsList(searchString));

                default:
                    Console.WriteLine("Default case");  //----> View Header title
                    ViewData["searchType"] = null;
                    ViewData["Title"] = "All British Bird Species";
                    return View(await _birdRepository.AllBirdsList(searchString));
            }
        }

        // GET: Bird/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = new BirdDetailViewModel()
            {
                Bird = await _birdRepository.GetBirdDetails(id),
            };

            model.BirdPhotos = _flickrService.GetFlickrPhotoCollection(model.Bird.Species);

            if (model.Bird == null)
            {
                return NotFound();
            }
            return View(model);
        }
    }
}