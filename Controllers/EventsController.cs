using Microsoft.AspNetCore.Mvc;
using AICalendar.Models;
using Microsoft.EntityFrameworkCore;

namespace AICalendar.Controllers
{
    [Route("api/v1/events")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly CalendarContext _context;

        public EventsController(CalendarContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _context.Events.Include(e => e.Participants).ToListAsync();
            return Ok(events);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(Event ev)
        {
            _context.Events.Add(ev);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEvents), new { id = ev.Id }, ev);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, Event ev)
        {
            if (id != ev.Id) return BadRequest();
            _context.Entry(ev).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return NotFound();
            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}