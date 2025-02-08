using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class CodePublication
    {
        public CodePublication() { }
        public CodePublication(
            string description,
            string code,
            string lang,
            decimal rating,
            Guid creatorId,
            User creator,
            DateTime postedDate
        )
        {
            Description = description;
            Code = code;
            Lang = lang;
            Rating = rating;
            CreatorId = creatorId;
            Creator = creator;
            PostedDate = postedDate;
        }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.Empty;
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Lang { get; set; } = string.Empty;
        public decimal Rating { get; set; }
        public List<Comment> Comments { get; set; } = [];
        public DateTime PostedDate { get; set; }
        public Guid CreatorId { get; set; } = Guid.Empty;
        public User Creator { get; set; } = new User();
    }
}