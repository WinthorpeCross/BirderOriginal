using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Birder2.Services;
using Microsoft.AspNetCore.Authorization;
using Birder2.ViewModels;
using Microsoft.Extensions.Logging;
using Birder2.Extensions;

/*
 * Perhaps use IEnumerable - not IQueryable -  for the Birds list because it is likely to be repeatedly searched and filtered?
 * Total size is just 600 rows.
*/

namespace Birder2.Controllers
{
    // ToDo: --Various--
    /*
     * ViewModel
     * Filter Options
     * 
     */

    [Authorize]
    public class BirdController : Controller
    {
        private readonly IBirdRepository _birdRepository;
        private readonly IFlickrService _flickrService;
        private readonly ILogger _logger;

        public BirdController(IBirdRepository birdRepository, IFlickrService flickrService
                                ,ILogger<BirdController> logger)
        {
            _birdRepository = birdRepository;
            _flickrService = flickrService;
            _logger = logger;
        }

        public class SortFilterPageOptions
        {
            public string uio { get; set; }
        }


        // GET: All Bird Species
        public async Task<IActionResult> Index(string searchString, string searchType, SortFilterPageOptions options)
        {
            _logger.LogInformation(LoggingEvents.ListItems, "Bird Index called");
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
            _logger.LogInformation(LoggingEvents.GetItem, "Getting bird {ID}", id);
            if (id == null)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, "GetById({ID}) ID PARAMETER IS NULL", id);
                return NotFound();
            }

            var model = new BirdDetailViewModel()
            {
                Bird = await _birdRepository.GetBirdDetails(id),

            };
            model.BirdPhotos = _flickrService.GetFlickrPhotoCollection(model.Bird.Species);

            if (model.Bird == null)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, "GetById({ID}) BIRD NOT FOUND", id);
                return NotFound();
            }
            try
            {
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(1, ex, "GetById({ID}) NOT FOUND", id);
                return NotFound();
            }
        }
    }
}