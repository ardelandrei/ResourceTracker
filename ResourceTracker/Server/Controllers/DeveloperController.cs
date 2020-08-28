using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourceTracker.Server.DataAccess;
using ResourceTracker.Shared;

namespace ResourceTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public DeveloperController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var devs = await _context.Developers.ToListAsync();
            return Ok(devs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var dev = await _context.Developers.FirstOrDefaultAsync(a => a.Id == id);

            if (dev == null)
            {
                return NotFound();
            }

            return Ok(dev);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Developer developer)
        {
            _context.Add(developer);
            var entriesAdded = await _context.SaveChangesAsync();
            if (entriesAdded > 0)
            {
                return Created(@"Get + {developer.Id}", entriesAdded);
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put(Developer developer)
        {
            _context.Entry(developer).State = EntityState.Modified;

            var entriesAdded = await _context.SaveChangesAsync();
            if (entriesAdded > 0)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var dev = new Developer { Id = id };
            _context.Remove(dev);

            var entriesAdded = await _context.SaveChangesAsync();
            if (entriesAdded > 0)
            {
                return NoContent();
            }

            return NotFound();
        }

    }
}
