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
            try
            {
                if (options.page == 0)
                {
                    options.page = 1;
                }
                BirdIndexViewModel viewModel = new BirdIndexViewModel();
                viewModel.AllBirdsDropDownList = _birdRepository.AllBirdsDropDownList();
                if (options.SelectedBirdId == 0)
                {
                    if (options.BirdStatusFilter == BirdIndexStatusFilter.Common)
                    {
                        viewModel.BirdsList = await _birdRepository.CommonBirdsList().GetPaged(options.page, pageSize);
                        viewModel.BirdStatusFilter = options.BirdStatusFilter;
                        viewModel.ListFormat = options.ListFormat;
                    }
                    else
                    {
                        viewModel.BirdsList = await _birdRepository.AllBirdsList().GetPaged(options.page, pageSize);
                        viewModel.BirdStatusFilter = options.BirdStatusFilter;
                        //viewModel.ShowAllBirds = true;  //options.ShowAllBirds;
                        viewModel.ListFormat = options.ListFormat;
                    }
                }
                else
                {
                    viewModel.BirdsList = await _birdRepository.AllBirdsList(options.SelectedBirdId).GetPaged(options.page, pageSize);
                    viewModel.SelectedBirdId = options.SelectedBirdId;
                    viewModel.ListFormat = options.ListFormat;
                }
                return View(viewModel);
            }
            catch (Exception ex)
            {
                // What to log?
                _logger.LogError(LoggingEvents.GetItemNotFound, ex, "Index({birdId}) error", options.SelectedBirdId);
                //_logger.LogError($"failed to return Birds details page: {ex}");//  <--
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

            var model = new BirdDetailViewModel();

            try
            {
                model.Bird = await _birdRepository.GetBirdDetails(id);
                model.BirdPhotos = _flickrService.GetFlickrPhotoCollection(model.Bird.Species);

                if (model.Bird == null)
                {
                    _logger.LogWarning(LoggingEvents.GetItemNotFound, "Details({ID}) BIRD NOT FOUND", id);
                    return NotFound();
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                // What to log?
                _logger.LogError(LoggingEvents.GetItemNotFound, ex, "Details({ID}) error", id);
                //_logger.LogError($"failed to return Birds details page: {ex}");//  <--
                return NotFound();
            }
        }
    }
}