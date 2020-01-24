using commentsApiAspNet.Domain.Core;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace commentsApiAspNet.Domain.Interfaces
{
    public interface ITokenRepository
    {
        Task<IEnumerable<Token>> GetList();
        Task<Token> Get(string id);
        //метод предназначен исключительно для заполнения БД.
        Task Save(Token item);
        Task<ReplaceOneResult> Update(string id, Token item);
        Task<DeleteResult> Delete(string id);
    }
}