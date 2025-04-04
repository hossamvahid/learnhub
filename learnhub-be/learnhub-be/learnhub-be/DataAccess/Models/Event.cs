namespace learnhub_be.DataAccess.Models
{
    public class Event
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public int CreatorId { get; set; }

        public DateTime Date { get; set; }

        public User? Creator { get; }

        public ICollection<Curricula>? Curriculas { get; }

        public ICollection<Participant>? Participants { get; }
    }
}
