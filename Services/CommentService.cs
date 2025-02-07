using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Abstract.Service;
using API.Models;

namespace API.Services
{
    public class CommentService : ICommentService
    {
        public Task<Guid> CreateComment(Comment codePublication)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> DeleteComment(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Comment>> GetAllComments()
        {
            throw new NotImplementedException();
        }

        public Task<Comment> GetComment(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}