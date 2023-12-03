using events_back.Context;
using events_back.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace events_back.Controllers
{
    [ApiController]
    [Route("participant")]
    public class ParticipantController : Controller
    {
        private readonly EventContext _eventContext;

        public ParticipantController(EventContext eventContext)
        {
            _eventContext = eventContext;
        }

        [HttpGet()]
        public async Task<ActionResult<List<Participant>>> GetAll()
        {
            var result = await _eventContext.Participants.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{paticipantId}")]
        public async Task<ActionResult<Participant>> GetById(int paticipantId)
        {
            var participantdb = await _eventContext.Participants.FirstOrDefaultAsync(x => x.Id.Equals(paticipantId));
            if(participantdb == null)
                return NotFound("Participant Does Not Exist");

            return Ok(participantdb);
        }

        [HttpPost()]
        public async Task<ActionResult<Participant>> Save([FromBody] Participant participantToSave){
            participantToSave.DateCreation = DateTime.Now;
            participantToSave.LastUpdate = DateTime.Now;
            await _eventContext.Participants.AddAsync(participantToSave);
            _eventContext.SaveChanges();
            return Ok(participantToSave);
        }

        [HttpPut()]
        public async Task<ActionResult<Participant>> Update([FromBody] Participant participantToUpdate){
            var participantdb = await _eventContext.Participants.FirstOrDefaultAsync(x => x.Id.Equals(participantToUpdate.Id));
            if(participantdb == null)
                return NotFound("Event Does Not Exist");

            participantdb.IDWControl = participantToUpdate.IDWControl;
            participantdb.Control = participantToUpdate.Control;
            participantdb.Name = participantToUpdate.Name;
            participantdb.LastUpdate = DateTime.Now;
            if(participantToUpdate.ParticipantInEvents != null)
                participantdb.ParticipantInEvents = participantToUpdate.ParticipantInEvents;

            _eventContext.SaveChanges();
            return Ok(participantToUpdate);
            
            
        }

        [HttpDelete("{eventId}")]
        public async Task<ActionResult<Participant>> Delete(int partipantId){
            var participantdb = await _eventContext.Participants.FirstOrDefaultAsync(x => x.Id.Equals(partipantId));
            if(participantdb == null)
                return NotFound("Event Does Not Exist");

            _eventContext.Remove(participantdb);
            _eventContext.SaveChanges();
            return Ok(participantdb);
        }
    }
}