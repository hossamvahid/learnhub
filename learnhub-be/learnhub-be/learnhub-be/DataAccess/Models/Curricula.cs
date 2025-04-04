namespace learnhub_be.DataAccess.Models
{
    public class Curricula
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Summarize { get; set; }

        public string? Filename { get; set; }

        public int EventId { get; set; }

        public int CreatorId { get; set; }

        public Event? Event { get; }

        public User? Creator { get; }
    }
}
