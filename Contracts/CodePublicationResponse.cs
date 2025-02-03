using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Contracts
{
    public record CodePublicationResponse(
        Guid Id,
        string Description,
        string Code,
        string Lang,
        DateTime PostedTime
    );
}