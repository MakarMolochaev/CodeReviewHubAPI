using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class User
    {
        public User() { }


        public Guid Id { get; set; }
        public string Nickname { get; set; } = string.Empty;
        public List<CodePublication> Publications { get; set; } = [];
    }
}