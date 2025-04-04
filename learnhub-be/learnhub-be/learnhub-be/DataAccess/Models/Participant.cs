namespace learnhub_be.DataAccess.Models
{
    public class Participant
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int EventId { get; set; }

        public User User { get; }

        public Event Event { get; }
    }
}
