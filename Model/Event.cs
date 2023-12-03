using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using events_back.DTO;

namespace events_back.Model
{
    [Table("Event")]
    public class Event
    {
        
        [Key]
        [Column("EventId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime LastUpdate { get; set; }
        [JsonIgnore]
        public ICollection<ParticipantInEvent>? ParticipantInEvents { get; set; }

        
        
        public Event(){ }
        public Event(EventDTO eventDTO, bool isSave)
        {
            this.Name = eventDTO.Name;
            this.Description = eventDTO.Description;
            this.LastUpdate = DateTime.Now;
            if(isSave)
                this.DateCreation = DateTime.Now;
        }
    }
}