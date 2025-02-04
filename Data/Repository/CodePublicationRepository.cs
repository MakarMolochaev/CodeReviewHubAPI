using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Abstract;
using API.Abstract.Repository;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repository
{
    public class CodePublicationRepository : ICodePublicationRepository
    {
        public readonly CodeReviewHubDbContext _context;

        public CodePublicationRepository(CodeReviewHubDbContext context)
        {
            _context = context;
        }

        public async Task<List<CodePublication>> GetAll()
        {
            var codePublications = await _context.CodePublications
                .ToListAsync();

            return codePublications;
        }

        public async Task<CodePublication> Get(Guid id)
        {
            var codePublication = await _context.CodePublications
                .FirstOrDefaultAsync(el => el.Id == id);

            return codePublication;
        }

        public async Task<Guid> Create(CodePublication codePublication)
        {
            await _context.CodePublications.AddAsync(codePublication);
            await _context.SaveChangesAsync();

            return codePublication.Id;
        }

        public async Task<Guid> Update(Guid id, string description, string code, string lang)
        {
            await _context.CodePublications
                .Where(b => b.Id == id)
                .ExecuteUpdateAsync(el => el
                    .SetProperty(b => b.Description, b => description)
                    .SetProperty(b => b.Code, b => code)
                    .SetProperty(b => b.Lang, b => lang)
                    );


            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.CodePublications
                .Where(b => b.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}