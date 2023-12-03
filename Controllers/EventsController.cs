using Microsoft.AspNetCore.Mvc;
using events_back.Model;
using events_back.Context;
using Microsoft.EntityFrameworkCore;
using events_back.DTO;

namespace events_back.Controllers
{
    [ApiController]
    [Route("events")]
    public class EventsController : ControllerBase
    {
        private readonly EventContext _eventContext;

        public EventsController(EventContext eventContext)
        {
            _eventContext = eventContext;
        }

        [HttpGet()]
        public async Task<ActionResult<List<Event>>> GetAll()
        {
            var result = await _eventContext.Events.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{eventId}")]
        public async Task<ActionResult<Event>> GetById(int eventId)
        {
            var eventdb = await _eventContext.Events.FirstOrDefaultAsync(x => x.Id.Equals(eventId));
            if(eventdb == null)
                return NotFound("Event Does Not Exist");

            return Ok(eventdb);
        }

        [HttpPost()]
        public async Task<ActionResult<Event>> Save([FromBody] EventDTO eventDTO){
            Event eventToSave = new Event(eventDTO, true);
            await _eventContext.Events.AddAsync(eventToSave);
            await _eventContext.SaveChangesAsync();
            return Ok(eventToSave);
        }

        [HttpPut()]
        public async Task<ActionResult<Event>> Update([FromBody] Event eventToUpdate){
            var eventdb = await _eventContext.Events.FirstOrDefaultAsync(x => x.Id.Equals(eventToUpdate.Id));
            if(eventdb == null)
                return NotFound("Event Does Not Exist");

            eventdb.Name = eventToUpdate.Name;
            eventdb.Description = eventToUpdate.Description;
            eventdb.LastUpdate = DateTime.Now;
            if(eventToUpdate.ParticipantInEvents != null)
                eventdb.ParticipantInEvents = eventToUpdate.ParticipantInEvents;

            await _eventContext.SaveChangesAsync();
            return Ok(eventToUpdate);
            
            
        }

        [HttpDelete("{eventId}")]
        public async Task<ActionResult<Event>> Delete(int eventId){
            var eventdb = await _eventContext.Events.FirstOrDefaultAsync(x => x.Id.Equals(eventId));
            if(eventdb == null)
                return NotFound("Event Does Not Exist");

            _eventContext.Remove(eventdb);
            await _eventContext.SaveChangesAsync();
            return Ok(eventdb);
        }
    }
}