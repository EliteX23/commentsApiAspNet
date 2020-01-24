﻿using commentsApiAspNet.Domain.Core;
using commentsApiAspNet.Domain.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace commentsApiAspNet.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private IMongoCollection<Comment> _collection;
        public CommentRepository(IMongoDatabase database)
        {
            if (_collection == null)
            {           
            _collection = database.GetCollection<Comment>("Comments");
            }
        }
        public async Task Create(Comment item)
        {
            await _collection.InsertOneAsync(item);
        }

        public async Task<DeleteResult> Delete(string id)
        {
            return await _collection.DeleteOneAsync(comment => comment.Id == id);

        }


        public async Task<Comment> GetComment(string id)
        {
            return await _collection.FindAsync(comment => comment.Id == id).Result.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentList()
        {
            return await _collection.FindAsync(book => true).Result.ToListAsync();
        }

        public async Task<ReplaceOneResult> Update(string id, Comment commentIn)
        {
            //можно использовать update, но не указано какие поля можем обновлять, по умолчанию обновляются все
            return await _collection.ReplaceOneAsync(comment => comment.Id == id, commentIn);
        }
    }
}