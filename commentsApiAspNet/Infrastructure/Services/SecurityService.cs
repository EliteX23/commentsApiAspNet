using commentsApiAspNet.Domain;
using commentsApiAspNet.Domain.Core;
using commentsApiAspNet.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace commentsApiAspNet.Infrastructure.Services
{
    public class SecurityService : IToken
    {
        private ITokenRepository _apiKeyRepo;
        public SecurityService(ITokenRepository apiRepo)
        {
            _apiKeyRepo = apiRepo;
        }

        public async Task Delete(string id)
        {
            await _apiKeyRepo.Delete(id);
        }

        public async Task<Token> GetApiKey(string id)
        {
            return await _apiKeyRepo.Get(id);
        }

        public async Task<IEnumerable<Token>> GetList()
        {
            return await _apiKeyRepo.GetList();
        }

        public async Task<bool> IsValid(string apikey, string method)
        {
            var apiKeyEntity = await _apiKeyRepo.Get(apikey);
            if (apiKeyEntity == null || !apiKeyEntity.IsActive)
            {
                return false;
            }
            if (!apiKeyEntity.GrantedRequest.Contains(method))
            {
                return false;
            }
            return true;
        }

        public async Task Save(Token key)
        {
            await _apiKeyRepo.Save(key);
        }

        public async Task Update(string id, Token item)
        {
            await _apiKeyRepo.Update(id, item);
        }
    }
}