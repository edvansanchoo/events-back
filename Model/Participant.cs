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
    [Table("Participant")]
    public class Participant
    {
        
        [Key]
        [Column("ParticipantId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Control { get; set; }
        public string? IDWControl { get; set; }
        public string? Name { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime LastUpdate { get; set; }
        public ICollection<ParticipantInEvent>? ParticipantInEvents { get; set; }


        public Participant(){}
        public Participant(ParticipantDTO participantDTO, bool isSave)
        {
            this.Control = participantDTO.Control;
            this.IDWControl = participantDTO.IDWControl;
            this.Name = participantDTO.Name;
            this.LastUpdate = DateTime.Now;
            if(isSave)
                this.DateCreation = DateTime.Now;
        }
    }
}