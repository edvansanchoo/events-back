using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace events_back.Model
{
    [Table("Event")]
    public class Event
    {
        [Key]
        [Column("EventId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        public DateTime DateCreation { get; set; }
        [JsonIgnore]
        public ICollection<ParticipantInEvent>? ParticipantInEvents { get; set; }
        [JsonIgnore]
        public DateTime LastUpdate { get; set; }
    }
}