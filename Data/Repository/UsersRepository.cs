using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repository
{
    public class UsersRepository
    {
        CodeReviewHubDbContext _context;
        public UsersRepository(CodeReviewHubDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Add(User user)
        {
            //await _context.Users.ExecuteDeleteAsync();
            //return new Guid();
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user.Id;
        }

        public async Task<User?> GetByEmail(string email)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);


            return user;
        }

        public async Task<User?> GetById(Guid id)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<List<User>> GetAll()
        {
            var users = await _context.Users
                .AsNoTracking()
                .ToListAsync();

            return users;
        }
    }
}