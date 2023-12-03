using Microsoft.AspNetCore.Mvc;
using events_back.Model;
using events_back.Context;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<Event>> Save([FromBody] Event eventToSave){
            eventToSave.DateCreation = DateTime.Now;
            eventToSave.LastUpdate = DateTime.Now;
            await _eventContext.Events.AddAsync(eventToSave);
            _eventContext.SaveChanges();
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

            _eventContext.SaveChanges();
            return Ok(eventToUpdate);
            
            
        }

        [HttpDelete("{eventId}")]
        public async Task<ActionResult<Event>> Delete(int eventId){
            var eventdb = await _eventContext.Events.FirstOrDefaultAsync(x => x.Id.Equals(eventId));
            if(eventdb == null)
                return NotFound("Event Does Not Exist");

            _eventContext.Remove(eventdb);
            _eventContext.SaveChanges();
            return Ok(eventdb);
        }
    }
}