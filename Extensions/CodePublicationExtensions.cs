using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Contracts;
using API.Models;

namespace API.Extensions
{
    public static class CodePublicationExtensions
    {
        public static CodePublicationResponse ToResponse(this CodePublication publication)
        {
            return new CodePublicationResponse(
                publication.Id,
                publication.Description,
                publication.Code,
                publication.Lang,
                publication.Rating,
                publication.PostedDate,
                publication.Creator.ToResponse(),
                publication.RatedUsers
            );
        }
    }
}