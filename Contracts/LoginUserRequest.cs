using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Contracts
{
    public record LoginUserRequest(
        string Email,
        string Password
    );
}