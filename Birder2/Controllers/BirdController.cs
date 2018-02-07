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