using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Abstract.Repository;
using API.Data;
using API.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class RatingService
    {
        
        public readonly CodeReviewHubDbContext _context;

        public RatingService(CodeReviewHubDbContext context)
        {
            _context = context;
        }

        public async Task RatePublication(Guid publicationId, Guid userId)
        {
            //var publication = await _context.CodePublications.FindAsync(publicationId);
            var publication = await _context.CodePublications.FirstOrDefaultAsync(el => el.Id == publicationId && !el.RatedUsers.Contains(userId));
            if (publication == null)
            {
                return;
            }

            publication.RatedUsers.Add(userId);
            publication.Rating += 1;
            await _context.SaveChangesAsync();           
        }

        public async Task UnratePublication(Guid publicationId, Guid userId)
        {
            //var publication = await _context.CodePublications.FindAsync(publicationId);
            var publication = await _context.CodePublications.FirstOrDefaultAsync(el => el.Id == publicationId && el.RatedUsers.Contains(userId));
            if (publication == null)
            {
                return;
            }

            publication.RatedUsers.Remove(userId);
            publication.Rating -= 1;
            await _context.SaveChangesAsync();           
        }
    }
}