using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace events_back.DTO
{
    public class ParticipantInEventDTO
    {
        public int EventId { get; set; }
        public int ParticipantId { get; set; }
        public string? ParticipantControl { get; set; }
        public string? ParticipantIDW { get; set; }

    }
}