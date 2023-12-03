using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using events_back.Context;
using events_back.DTO;
using events_back.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace events_back.Controllers
{
    [ApiController]
    [Route("participantInEvent")]
    public class ParticipantInEventController : Controller
    {
        private readonly EventContext _eventContext;

        public ParticipantInEventController(EventContext eventContext)
        {
            _eventContext = eventContext;
        }

        [HttpGet()]
        public async Task<ActionResult<List<ParticipantInEvent>>> GetAll()
        {
            var result = await _eventContext.ParticipantInEvents.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ParticipantInEvent>> GetById(int id)
        {
            var participantInEventsdb = await _eventContext.ParticipantInEvents.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if(participantInEventsdb == null)
                return NotFound("Information Does Not Exist");

            return Ok(participantInEventsdb);
        }

        [HttpPost()]
        public async Task<ActionResult<ParticipantInEvent>> Save([FromBody] ParticipantInEventDTO participantInEventDTO){
            try
            {
                var eventdb = await _eventContext.Events.FirstOrDefaultAsync(x => x.Id.Equals(participantInEventDTO.EventId));
                if(eventdb == null)
                    return NotFound("Event Not Found");
                
                var participantdb = await _eventContext.Participants.FirstOrDefaultAsync(x => x.Id.Equals(participantInEventDTO.ParticipantId));
                if(participantdb == null)
                    return NotFound("Partipant Not Found");

                ParticipantInEvent participantToSave = new ParticipantInEvent(participantInEventDTO);
                await _eventContext.ParticipantInEvents.AddAsync(participantToSave);
                await _eventContext.SaveChangesAsync();
                return Ok(participantToSave);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException && sqlException.Number == 2627)
                {
                    return Conflict("The Participant has already been registered for this event.");
                }

                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        [HttpPut()]
        public async Task<ActionResult<ParticipantInEvent>> Update([FromBody] ParticipantInEvent participantToSave){
            var participantInEventdb = await _eventContext.ParticipantInEvents.FirstOrDefaultAsync(x => x.Id.Equals(participantToSave.Id));
            if(participantInEventdb == null)
                return NotFound("ParticipantInEvent Not Found");

            var eventdb = await _eventContext.Events.FirstOrDefaultAsync(x => x.Id.Equals(participantToSave.EventId));
            if(eventdb == null)
                return NotFound("Event Not Found");
            
            var participantdb = await _eventContext.Participants.FirstOrDefaultAsync(x => x.Id.Equals(participantToSave.ParticipantId));
            if(participantdb == null)
                return NotFound("Partipant Not Found");


            participantInEventdb.LastUpdate = DateTime.Now;
            participantInEventdb.Event = eventdb;
            participantInEventdb.Participant = participantdb;

            await _eventContext.ParticipantInEvents.AddAsync(participantInEventdb);
            await _eventContext.SaveChangesAsync();
            return Ok(participantToSave);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Event>> Delete(int id){
            var participantInEventdb = await _eventContext.ParticipantInEvents.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if(participantInEventdb == null)
                return NotFound("ParticipantInEvent Not Found");

            _eventContext.Remove(participantInEventdb);
            await _eventContext.SaveChangesAsync();
            return Ok(participantInEventdb);
        }
    }
}