using commentsApiAspNet.Domain.Core;
using commentsApiAspNet.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace commentsApiAspNet.Infrastructure.Services
{
    public class CommentService : IComment
    {
        private ICommentRepository _commentRepo;


        public CommentService(ICommentRepository commentRepo)
        {
            if (_commentRepo == null) { 
            _commentRepo = commentRepo;
            }
        }
        public async Task Delete(string id)
        {
            await _commentRepo.Delete(id);
        }

        public async Task<Comment> GetComment(string id)
        {
            return await _commentRepo.GetComment(id);
        }

        public async Task<IEnumerable<Comment>> GetList()
        {
            return await _commentRepo.GetCommentList();
        }

        public async Task Save(Comment comment)
        {
            await _commentRepo.Create(comment);
        }

        public async Task Update(string id, Comment comment)
        {
            await _commentRepo.Update(id, comment);
        }
    }
}