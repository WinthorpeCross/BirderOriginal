using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Birder2.Data;
using Birder2.Models;

namespace Birder2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObservationApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ObservationApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ObservationApi
        [HttpGet]
        public IEnumerable<Observation> GetObservations()
        {
            return _context.Observations;
        }

        // GET: api/ObservationApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetObservation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var observation = await _context.Observations.FindAsync(id);

            if (observation == null)
            {
                return NotFound();
            }

            return Ok(observation);
        }

        // PUT: api/ObservationApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutObservation([FromRoute] int id, [FromBody] Observation observation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != observation.ObservationId)
            {
                return BadRequest();
            }

            _context.Entry(observation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ObservationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ObservationApi
        [HttpPost]
        public async Task<IActionResult> PostObservation([FromBody] Observation observation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Observations.Add(observation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetObservation", new { id = observation.ObservationId }, observation);
        }

        // DELETE: api/ObservationApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteObservation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var observation = await _context.Observations.FindAsync(id);
            if (observation == null)
            {
                return NotFound();
            }

            _context.Observations.Remove(observation);
            await _context.SaveChangesAsync();

            return Ok(observation);
        }

        private bool ObservationExists(int id)
        {
            return _context.Observations.Any(e => e.ObservationId == id);
        }
    }
}