using Birder2.Extensions;
using Birder2.Services;
using Birder2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Birder2.Controllers
{
    [Authorize]
    public class BirdController : Controller
    {
        private const int pageSize = 12;
        private readonly IBirdRepository _birdRepository;
        private readonly IFlickrService _flickrService;
        private readonly ILogger _logger;

        public BirdController(IBirdRepository birdRepository,
                                IFlickrService flickrService,
                                    ILogger<BirdController> logger)
        {
            _birdRepository = birdRepository;
            _flickrService = flickrService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(SortFilterBirdIndexOptions options)
        {
            _logger.LogInformation(LoggingEvents.ListItems, "Bird Index called");

            if (options.Page == 0)
            {
                options.Page = 1;
            }

            if (!ModelState.IsValid)
            {
                var errorMessages = ModelStateErrorsExtensions.GetModelStateErrorMessages(ModelState);
                _logger.LogInformation(LoggingEvents.GetItem, "ModelState error with form values: {0}", errorMessages);
                return BadRequest("ModelState error with form values: " + errorMessages);
            }

            try
            { 
                var viewModel = new BirdIndexViewModel();
                viewModel.AllBirdsDropDownList = _birdRepository.AllBirdsDropDownList();

                if (options.SelectedBirdId != 0)
                {
                    return RedirectToAction("Details", new { id = options.SelectedBirdId });
                }
                else
                { 
                    if (options.BirdStatusFilter == BirdIndexStatusFilter.Common)
                    {
                        viewModel.BirdsList = await _birdRepository.CommonBirdsList().GetPaged(options.Page, options.SelectedPageListSize);
                        viewModel.ListFormat = options.ListFormat;
                    }
                    else
                    {
                        viewModel.BirdsList = await _birdRepository.AllBirdsList().GetPaged(options.Page, options.SelectedPageListSize);
                        viewModel.ListFormat = options.ListFormat;
                    }
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.GetItemNotFound, ex, "Index({birdId}) error", options.SelectedBirdId);
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            _logger.LogInformation(LoggingEvents.GetItem, "Getting bird {ID}", id);
            if (id == null)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, "Details({ID}) - ID PARAMETER IS NULL", id);
                return NotFound();
            }

            try
            {
                var ViewModel = new BirdDetailViewModel();
                ViewModel.Bird = await _birdRepository.GetBirdDetails(id);
                ViewModel.BirdPhotos = _flickrService.GetFlickrPhotoCollection(ViewModel.Bird.Species);

                if (ViewModel.Bird == null)
                {
                    _logger.LogWarning(LoggingEvents.GetItemNotFound, "Details({ID}) BIRD NOT FOUND", id);
                    return NotFound();
                }

                return View(ViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.GetItemNotFound, ex, "Details({ID}) error", id);
                return NotFound();
            }
        }
    }
}