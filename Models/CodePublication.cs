using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class CodePublication
    {
        public CodePublication() { }
        public CodePublication(Guid id, string description, string code, string lang, DateTime postedTime)
        {
            Id = id;
            Description = description;
            Code = code;
            Lang = lang;
            PostedDate = postedTime;
        }

        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Lang { get; set; } = string.Empty;
        public decimal Rating { get; set; }
        public List<Comment> Comments { get; set; } = [];
        public DateTime PostedDate { get; set; }
        public Guid CreatorId { get; set; }
        public User Creator { get; set; }
    }
}