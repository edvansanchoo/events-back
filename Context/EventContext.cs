using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using events_back.Model;
using events_back.Utils;
using Microsoft.EntityFrameworkCore;

namespace events_back.Context
{
    public class EventContext : DbContext
    {
        public DbSet<Event> Events {get;set;}
        public DbSet<Participant> Participants {get;set;}
        public DbSet<ParticipantInEvent> ParticipantInEvents {get;set;}

        public EventContext(DbContextOptions<EventContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ParticipantInEvent>()
                .HasKey(c => new {c.ParticipantId, c.EventId});

             modelBuilder.Entity<Participant>()
                .HasIndex(e => e.Control)
                .IsUnique();

            modelBuilder.Entity<Participant>()
                .HasIndex(e => e.IDWControl)
                .IsUnique();
        }
    }
}