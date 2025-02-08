using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Abstract.Repository;
using API.Abstract.Service;
using API.Models;

namespace API.Services
{
    public class CommentService : ICommentService
    {
        public readonly ICommentRepository _commentRespository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRespository = commentRepository;
        }

        public async Task<Guid> CreateComment(Comment codePublication)
        {
            var id = await _commentRespository.Create(codePublication);
            return id;
        }

        public async Task<Guid> DeleteComment(Guid id)
        {
            await _commentRespository.Delete(id);
            return id;
        }

        public async Task<List<Comment>> GetAllComments()
        {
            var comments = await _commentRespository.GetAll();

            return comments;
        }

        public async Task<Comment?> GetComment(Guid id)
        {
            var comment = await _commentRespository.Get(id);
            return comment;
        }
    }
}