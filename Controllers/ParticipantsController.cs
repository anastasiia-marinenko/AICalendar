using Microsoft.AspNetCore.Mvc;
using AICalendar.Models;
using Microsoft.EntityFrameworkCore;

namespace AICalendar.Controllers
{
    [Route("api/v1/events/{eventId}/participants")]
    [ApiController]
    public class ParticipantsController : ControllerBase
    {
        private readonly CalendarContext _context;

        public ParticipantsController(CalendarContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetParticipants(int eventId)
        {
            var participants = await _context.Participants
                .Where(p => p.EventId == eventId)
                .Include(p => p.User)
                .ToListAsync();
            return Ok(participants);
        }

        [HttpPost]
        public async Task<IActionResult> AddParticipant(int eventId, Participant participant)
        {
            participant.EventId = eventId;
            _context.Participants.Add(participant);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetParticipants), new { eventId = eventId, id = participant.Id }, participant);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> RemoveParticipant(int eventId, int userId)
        {
            var participant = await _context.Participants
                .FirstOrDefaultAsync(p => p.EventId == eventId && p.UserId == userId);
            if (participant == null) return NotFound();
            _context.Participants.Remove(participant);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}