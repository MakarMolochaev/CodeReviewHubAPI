using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Contracts;
using API.Models;

namespace API.Extensions
{
    public static class UserExtensions
    {
        public static UserResponse ToResponse(this User user)
        {
            return new UserResponse(
                user.Username,
                user.Id
            );
        }
    }
}