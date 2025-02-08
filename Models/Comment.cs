using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Comment
    {
        public Comment() { }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.Empty;
        public string Text { get; set; } = string.Empty;
        public Guid CreatorId { get; set; }
        public User Creator { get; set; }
    }
}