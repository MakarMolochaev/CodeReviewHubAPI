using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class User
    {
        public User() { }

        public User(Guid id, string username, string email, string passwordHash) 
        {
            Id = id;
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
        }
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public List<CodePublication> Publications { get; set; } = [];
    }
}