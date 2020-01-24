﻿using commentsApiAspNet.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace commentsApiAspNet.Domain.Interfaces
{
    //Интерфейс сервиса авторизации
    public interface IToken
    {
        Task<bool> IsValid(string apikey, string method);

        //Методы только для теста
        Task<Token> GetApiKey(string id);
        Task Save(Token key);
        Task Update(string id, Token item);
        Task Delete(string id);
    }
}