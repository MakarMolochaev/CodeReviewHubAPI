using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class User
    {
        public User() { }

        public User(string username, string email, string passwordHash) 
        {
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
        }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public List<CodePublication> Publications { get; set; } = new List<CodePublication>();
    }
}