using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Birder2.Data;
using Birder2.Models;

namespace Birder2.Controllers
{
    public class BirdController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BirdController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bird
        public async Task<IActionResult> Index()
        {
            return View(await _context.Birds.ToListAsync());
        }

        // GET: Bird/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bird = await _context.Birds
                .SingleOrDefaultAsync(m => m.BirdId == id);
            if (bird == null)
            {
                return NotFound();
            }

            return View(bird);
        }

        // GET: Bird/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bird/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BirdId,Image,EnglishName,InternationalName,ScientificName")] Bird bird)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bird);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bird);
        }

        // GET: Bird/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bird = await _context.Birds.SingleOrDefaultAsync(m => m.BirdId == id);
            if (bird == null)
            {
                return NotFound();
            }
            return View(bird);
        }

        // POST: Bird/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BirdId,Image,EnglishName,InternationalName,ScientificName")] Bird bird)
        {
            if (id != bird.BirdId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bird);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BirdExists(bird.BirdId))
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
            return View(bird);
        }

        // GET: Bird/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bird = await _context.Birds
                .SingleOrDefaultAsync(m => m.BirdId == id);
            if (bird == null)
            {
                return NotFound();
            }

            return View(bird);
        }

        // POST: Bird/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bird = await _context.Birds.SingleOrDefaultAsync(m => m.BirdId == id);
            _context.Birds.Remove(bird);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BirdExists(int id)
        {
            return _context.Birds.Any(e => e.BirdId == id);
        }
    }
}
