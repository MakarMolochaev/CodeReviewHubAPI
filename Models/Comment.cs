namespace API.Models
{
    public class Comment
    {
        public Comment() { }

        public Guid Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public Guid CreatorId { get; set; }
        public User Creator { get; set; }
    }
}