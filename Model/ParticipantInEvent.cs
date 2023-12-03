using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace events_back.Model
{
    [Table("ParticipantInEvent")]
    public class ParticipantInEvent
    {
        [Key]
        [Column("ParticipantInEventId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
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
        [JsonIgnore]
        public DateTime DateParticipation { get; set; }
        [JsonIgnore]
        public DateTime LastUpdate { get; set; }
    }
}