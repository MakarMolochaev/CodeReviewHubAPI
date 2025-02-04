using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace API.Abstract.Repository
{
    public interface IUsersRepository
    {
        Task<Guid> Add(User user);
        Task<User> GetByEmail(string email);
    }
}