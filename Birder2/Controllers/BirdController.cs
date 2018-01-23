using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Birder2.Services;

namespace Birder2.Controllers
{
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
            var bird = await _birdRepository.GetBirdDetails(id);

            if (bird == null)
            {
                return NotFound();
            }
            return View(bird);
        }
    }
}