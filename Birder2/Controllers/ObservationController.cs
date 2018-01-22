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

        public ObservationController(ApplicationDbContext context, IApplicationUserAccessor userAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
        }

        // GET: Observation
        public async Task<IActionResult> Index()
        {
            var user = await _userAccessor.GetUser();
            //var r = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var applicationDbContext = _context.Observations.Include(o => o.Bird);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Observation/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Observation/Create
        public IActionResult Create()
        {
            ViewData["BirdId"] = new SelectList(_context.Birds, "BirdId", "EnglishName");
            return View();
        }

        // POST: Observation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ObservationId,ObservationDateTime,Location,Note,BirdId,ApplicationUserId")] Observation observation)
        {
            var user = new ApplicationUser();
            if (ModelState.IsValid)
            {
                _context.Add(observation);
                user.Observations.Add(observation); // 
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BirdId"] = new SelectList(_context.Birds, "BirdId", "EnglishName", observation.BirdId);
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
