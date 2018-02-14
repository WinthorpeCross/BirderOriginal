using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Birder2.Models;
using Birder2.Services;
using Microsoft.AspNetCore.Authorization;
using Birder2.ViewModels;

namespace Birder2.Controllers
{
    [Authorize]
    public class x_ObservationController : Controller
    {
        private readonly IApplicationUserAccessor _userAccessor;
        private readonly IObservationRepository _observationRepository;
        private readonly IMachineClockDateTime _systemClock;

        public x_ObservationController(IApplicationUserAccessor userAccessor,
                                     IObservationRepository observationRepository,
                                     IMachineClockDateTime systemClock)
        {
            _userAccessor = userAccessor;
            _observationRepository = observationRepository;
            _systemClock = systemClock;
        }

        // GET: Observation
        //  **** This is My Observations.  Need to overload to request my + mates' observations
        public async Task<IActionResult> Index()
        {
            var user = await _userAccessor.GetUser();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(await _observationRepository.MyObservationsList(user)); // --> do not get user twice! await _userAccessor.GetUser()));
        }

        // GET: Observation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //ToDo: check for logged in user here?
            if (id == null)
            {
                return NotFound();
            }
            var observation = await _observationRepository.GetObservationDetails(id);

            if (observation == null)
            {
                return NotFound();
            }
            return View(observation);  //ToDo: if user == logged in user, then allow edit/delete etc.  Might need a viewmodel...
        }

        // GET: Observation/Create
        public async Task<IActionResult> Create()
        {
            var model = new CreateObservationViewModel()
            {
                Observation = new Observation() { Quantity = 1, ObservationDateTime = _systemClock.Now },
                // ToDo: include Birder category and sort so common species appear first...
                Birds = await _observationRepository.AllBirdsList()
            };
            return View(model);
        }

        // POST: Observation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ObservationId,ObservationDateTime,Location,Note,BirdId,LocationLatitude,LocationLongitude")] Observation observation)
        {
            var user = await _userAccessor.GetUser();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            observation.ApplicationUser = user;

            if (ModelState.IsValid)
            {
                try
                {
                    observation.CreationDate = _systemClock.Now;
                    observation.LastUpdateDate = _systemClock.Now;

                    await _observationRepository.AddObservation(observation);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    //ToDo: Logging / return user to create view, like below?
                    return NotFound("could not add the observation");
                }
            }
            var model = new CreateObservationViewModel()
            {
                Observation = observation,
                Birds = await _observationRepository.AllBirdsList()
            };

            return View(model);
        }

        // GET: Observation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var observation = await _observationRepository.GetObservationDetails(id);
            if (observation == null)
            {
                return NotFound();
            }

            try
            {
                var birds = await _observationRepository.AllBirdsList();
                ViewData["BirdId"] = new SelectList(birds, "BirdId", "EnglishName"); //, observation.BirdId);
                return View(observation);
            }
            catch
            {
                //ToDo: Logging / return user to create view, like below?
                return NotFound("could not add the observation");
            }
        }

        // POST: Observation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ObservationId,ObservationDateTime,Location,Note,BirdId,ApplicationUserId")] Observation observation)
        {
            // ToDo: Look into this update method.

            if (id != observation.ObservationId)
            {
                return NotFound();
            }

            var user = await _userAccessor.GetUser();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            observation.ApplicationUser = user;

            if (ModelState.IsValid)
            {
                try
                {
                    await _observationRepository.UpdateObservation(observation);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _observationRepository.ObservationExists(observation.ObservationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));  //return to details view?
            }
            var birds = await _observationRepository.AllBirdsList();
            ViewData["BirdId"] = new SelectList(birds, "BirdId", "EnglishName"); //, observation.BirdId);
            return View(observation);
        }

        // GET: Observation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var observation = await _observationRepository.GetObservationDetails(id);

            if (observation == null)
            {
                return NotFound();
            }

            return View(observation);
        }

        // POST: Observation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _observationRepository.DeleteObservation(id);
            }
            catch
            {
                //logging
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}


