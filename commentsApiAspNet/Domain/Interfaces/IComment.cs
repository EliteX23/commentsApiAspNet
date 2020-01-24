using commentsApiAspNet.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace commentsApiAspNet.Domain.Interfaces
{
    public interface IComment
    {
        Task<IEnumerable<Comment>> GetList();
        Task<Comment> GetComment(string id);
        Task Save(Comment comment);
        Task Update(string id, Comment item);
        Task Delete(string id);
    }
}