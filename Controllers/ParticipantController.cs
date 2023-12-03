using events_back.Context;
using events_back.DTO;
using events_back.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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

        [HttpGet("/Search/{control_Or_IDWControl}")]
        public async Task<ActionResult<Participant>> GetById(string control_Or_IDWControl)
        {
            var participantdb = await _eventContext.Participants
                .FirstOrDefaultAsync(x => (x.Control != null && x.Control.Equals(control_Or_IDWControl)) 
                                       || ( x.IDWControl != null && x.IDWControl.Equals(control_Or_IDWControl)));

            if(participantdb == null)
                return NotFound("Participant Does Not Exist");

            return Ok(participantdb);
        }

        [HttpPost()]
        public async Task<ActionResult<Participant>> Save([FromBody] ParticipantDTO participantDTO){
            try{
                    Participant participantToSave = new Participant(participantDTO, true);
                    await _eventContext.Participants.AddAsync(participantToSave);
                    await _eventContext.SaveChangesAsync();
                    return Ok(participantToSave);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException && sqlException.Number == 2627)
                {
                    return Conflict("The Participant has already been registered.");
                }

                if (IsUniqueConstraintViolation(ex, "IX_Participant_Control"))
                {
                    return Conflict("A participant with the same control or idwControl value already exists.");
                }

                return BadRequest(ex);
            }
        }

        [HttpPut()]
        public async Task<ActionResult<Participant>> Update([FromBody] Participant participantToUpdate){
            var participantdb = await _eventContext.Participants.FirstOrDefaultAsync(x => x.Id.Equals(participantToUpdate.Id));
            if(participantdb == null)
                return NotFound("Participant Does Not Exist");

            participantdb.IDWControl = participantToUpdate.IDWControl;
            participantdb.Control = participantToUpdate.Control;
            participantdb.Name = participantToUpdate.Name;
            participantdb.LastUpdate = DateTime.Now;
            if(participantToUpdate.ParticipantInEvents != null)
                participantdb.ParticipantInEvents = participantToUpdate.ParticipantInEvents;

            await _eventContext.SaveChangesAsync();
            return Ok(participantToUpdate);
        }

        [HttpDelete("{eventId}")]
        public async Task<ActionResult<Participant>> Delete(int partipantId){
            var participantdb = await _eventContext.Participants.FirstOrDefaultAsync(x => x.Id.Equals(partipantId));
            if(participantdb == null)
                return NotFound("Participant Does Not Exist");

            _eventContext.Remove(participantdb);
            await _eventContext.SaveChangesAsync();
            return Ok(participantdb);
        }


        private static bool IsUniqueConstraintViolation(DbUpdateException ex, string uniqueIndexName)
        {
            return ex.InnerException is SqlException sqlException &&
                sqlException.Number == 2601 &&
                sqlException.Message.Contains(uniqueIndexName);
        }
    }
}