using learnhub_be.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace learnhub_be
{
    public class PgSqlDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Curricula> Curriculas { get; set; }

        public DbSet<Participant> Participants { get; set; }

        public PgSqlDbContext() { }
        public PgSqlDbContext(DbContextOptions<PgSqlDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            //Event -> User (One-To-Many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.EventsCreated)
                .WithOne(e => e.Creator)
                .HasForeignKey(u => u.CreatorId);

            //Curricula -> User (One-To-Many)
            modelBuilder.Entity<Curricula>()
                .HasOne(c => c.Creator)
                .WithMany(u => u.Curriculas)
                .HasForeignKey(c => c.CreatorId);

            //Participants -> User (One-To-Many)
            modelBuilder.Entity<Participant>()
                .HasOne(p => p.User)
                .WithMany(u => u.EventsJoined)
                .HasForeignKey(p => p.UserId);



            //Curricula -> Event (One-To-Many)
            modelBuilder.Entity<Curricula>()
                .HasOne(c => c.Event)
                .WithMany(e => e.Curriculas)
                .HasForeignKey(c => c.EventId);

            //Participant -> Event (One-To-Many)
            modelBuilder.Entity<Participant>()
                .HasOne(p=>p.Event)
                .WithMany(e=>e.Participants)
                .HasForeignKey(p=>p.EventId);
                       
        }
    }
}
