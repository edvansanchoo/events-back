using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace events_back.Model
{
    [Table("Participant")]
    public class Participant
    {
        [Key]
        [Column("ParticipantId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }
        public string? Control { get; set; }
        public string? IDWControl { get; set; }
        public string? Name { get; set; }
        [JsonIgnore]
        public DateTime DateCreation { get; set; }
        [JsonIgnore]
        public DateTime LastUpdate { get; set; }
        [JsonIgnore]
        public ICollection<ParticipantInEvent>? ParticipantInEvents { get; set; }

    }
}