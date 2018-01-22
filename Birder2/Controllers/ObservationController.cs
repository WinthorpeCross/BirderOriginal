using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Birder2.Data;
using Birder2.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Birder2.Services;

namespace Birder2.Controllers
{
    public class ObservationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IApplicationUserAccessor _userAccessor;
        private readonly IObservationRepository _observationRepository;
        private readonly IMachineClockDateTime _systemClock;

        public ObservationController(ApplicationDbContext context,
                                     IApplicationUserAccessor userAccessor,
                                     IObservationRepository observationRepository,
                                     IMachineClockDateTime systemClock)
        {
            _context = context;
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
            var birds = await _observationRepository.AllBirdsList();
            ViewData["BirdId"] = new SelectList(birds, "BirdId", "EnglishName");
            return View();
        }

        // POST: Observation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ObservationId,ObservationDateTime,Location,Note,BirdId")] Observation observation)
        {
            var user = await _userAccessor.GetUser();
            observation.ApplicationUser = user;
            
            //if user == null....

            if (ModelState.IsValid)
            {
                observation.DateCreated = _systemClock.Now;
                _context.Add(observation);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            var birds = await _observationRepository.AllBirdsList();
            ViewData["BirdId"] = new SelectList(birds, "BirdId", "EnglishName", observation.BirdId);
            return View(observation);
        }

        // GET: Observation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var observation = await _context.Observations.SingleOrDefaultAsync(m => m.ObservationId == id);
            if (observation == null)
            {
                return NotFound();
            }
            ViewData["BirdId"] = new SelectList(_context.Birds, "BirdId", "EnglishName", observation.BirdId);
            return View(observation);
        }

        // POST: Observation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ObservationId,ObservationDateTime,Location,Note,BirdId,ApplicationUserId")] Observation observation)
        {
            if (id != observation.ObservationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(observation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ObservationExists(observation.ObservationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BirdId"] = new SelectList(_context.Birds, "BirdId", "EnglishName", observation.BirdId);
            return View(observation);
        }

        // GET: Observation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var observation = await _context.Observations
                .Include(o => o.Bird)
                .SingleOrDefaultAsync(m => m.ObservationId == id);
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
            var observation = await _context.Observations.SingleOrDefaultAsync(m => m.ObservationId == id);
            _context.Observations.Remove(observation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ObservationExists(int id)
        {
            return _context.Observations.Any(e => e.ObservationId == id);
        }
    }
}
