using API.Abstract.Repository;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repository
{
    public class CommentRepository : ICommentRepository
    {
        public readonly CodeReviewHubDbContext _context;
        public CommentRepository(CodeReviewHubDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Create(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return comment.Id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Comments
                .Where(el => el.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }

        public async Task<Comment?> Get(Guid id)
        {
            var comment = await _context.Comments
                .FirstOrDefaultAsync(el => el.Id == id);

            return comment;
        }

        public async Task<List<Comment>> GetAll()
        {
            var comments = await _context.Comments
                .ToListAsync();

            return comments;
        }
    }
}