using API.Models;

namespace API.Abstract.Repository
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAll();
        Task<Comment?> Get(Guid id);
        Task<Guid> Create(Comment comment);
        Task<Guid> Delete(Guid id);
    }
}