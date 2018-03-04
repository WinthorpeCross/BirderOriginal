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
    public class NetworksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NetworksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Networks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Network.Include(n => n.ApplicationUser).Include(n => n.Follower);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Networks/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var network = await _context.Network
                .Include(n => n.ApplicationUser)
                .Include(n => n.Follower)
                .SingleOrDefaultAsync(m => m.ApplicationUserId == id);
            if (network == null)
            {
                return NotFound();
            }

            return View(network);
        }

        // GET: Networks/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["FollowerId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Networks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicationUserId,FollowerId")] Network network)
        {
            if (ModelState.IsValid)
            {
                _context.Add(network);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", network.ApplicationUserId);
            ViewData["FollowerId"] = new SelectList(_context.Users, "Id", "Id", network.FollowerId);
            return View(network);
        }

        // GET: Networks/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var network = await _context.Network.SingleOrDefaultAsync(m => m.ApplicationUserId == id);
            if (network == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", network.ApplicationUserId);
            ViewData["FollowerId"] = new SelectList(_context.Users, "Id", "Id", network.FollowerId);
            return View(network);
        }

        // POST: Networks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ApplicationUserId,FollowerId")] Network network)
        {
            if (id != network.ApplicationUserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(network);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NetworkExists(network.ApplicationUserId))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", network.ApplicationUserId);
            ViewData["FollowerId"] = new SelectList(_context.Users, "Id", "Id", network.FollowerId);
            return View(network);
        }

        // GET: Networks/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var network = await _context.Network
                .Include(n => n.ApplicationUser)
                .Include(n => n.Follower)
                .SingleOrDefaultAsync(m => m.ApplicationUserId == id);
            if (network == null)
            {
                return NotFound();
            }

            return View(network);
        }

        // POST: Networks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var network = await _context.Network.SingleOrDefaultAsync(m => m.ApplicationUserId == id);
            _context.Network.Remove(network);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NetworkExists(string id)
        {
            return _context.Network.Any(e => e.ApplicationUserId == id);
        }
    }
}
