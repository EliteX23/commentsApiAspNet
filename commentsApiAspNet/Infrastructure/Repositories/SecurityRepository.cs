using commentsApiAspNet.Domain.Core;
using commentsApiAspNet.Domain.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace commentsApiAspNet.Infrastructure.Repositories
{
    public class SecurityRepository : ITokenRepository
    {
        private IMongoCollection<Token> _collection;

      
        public SecurityRepository(IMongoDatabase database)
        {                  
            _collection = database.GetCollection<Token>("Api");          
        }

        public async Task<DeleteResult> Delete(string id)
        {
            return await _collection.DeleteOneAsync(apiKey => apiKey.Key == id);
        }
        public async Task<IEnumerable<Token>> GetList()
        {
            return await _collection.FindAsync(book => true).Result.ToListAsync();
        }
        public async Task<Token> Get(string id)
        {
            return await _collection.FindAsync(apiKey => apiKey.Key == id).Result.FirstOrDefaultAsync();
        }

        public async Task Save(Token item)
        {
            await _collection.InsertOneAsync(item);
        }

        public async Task<ReplaceOneResult> Update(string id, Token item)
        {
            return await _collection.ReplaceOneAsync(apiKey => apiKey.Key == id, item);
        }
    }
}