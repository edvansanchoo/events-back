using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using events_back.DTO;

namespace events_back.Model
{
    [Table("ParticipantInEvent")]
    public class ParticipantInEvent
    {
        
        [Key]
        [Column("ParticipantInEventId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column("EventId")]
        [ForeignKey("Event")]
        
        public int EventId { get; set; }

        [Key]
        [Column("ParticipantId")]
        [ForeignKey("Participant")]
        public int ParticipantId { get; set; }
        [JsonIgnore]

        public Event? Event { get; set; }
        [JsonIgnore]

        public Participant? Participant { get; set; }
        public DateTime DateParticipation { get; set; }
        public DateTime LastUpdate { get; set; }

        public ParticipantInEvent(){}

        public ParticipantInEvent(ParticipantInEventDTO participantInEventDTO)
        {
            this.EventId = participantInEventDTO.EventId;
            this.ParticipantId = participantInEventDTO.ParticipantId;
            this.LastUpdate = DateTime.Now;
            this.DateParticipation = DateTime.Now;
        }
    }
}