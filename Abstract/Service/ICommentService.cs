using API.Models;

namespace API.Abstract.Service
{
    public interface ICommentService
    {
        Task<List<Comment>> GetAllComments();
        Task<Comment> GetComment(Guid id);
        Task<Guid> CreateComment(Comment codePublication);
        Task<Guid> DeleteComment(Guid id);
    }
}