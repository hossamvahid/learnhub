using System.ComponentModel.DataAnnotations;

namespace learnhub_be.DataAccess.Models
{
    public class User
    {
        public int Id { get; set; }

        public string? Username { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? Password { get; set; }

        public ICollection<Event>? EventsCreated { get; }

        public ICollection<Curricula>? Curriculas { get; }

        public ICollection<Participant>? EventsJoined { get; }


    }
}
