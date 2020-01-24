using commentsApiAspNet.Domain.Interfaces;
using Microsoft.Data.Edm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.OData.Routing;

namespace commentsApiAspNet.Middlewares
{
     public class TokenMiddleware : DelegatingHandler
    {
        private readonly IToken _securityService;
        public TokenMiddleware(IToken securityService)
        {
            _securityService = securityService;
        }

        protected override async System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            if (request.RequestUri.AbsolutePath.Contains("Comments"))
            {
                var token = request.Headers.Authorization;
                var methodType = request.Method.Method;
                if (token ==null || token.ToString().Length == 0)
                {
                    return await Task.FromResult<HttpResponseMessage>(
                          request.CreateResponse(HttpStatusCode.Unauthorized));
                }
                var isTokenValid = await _securityService.IsValid(token.ToString(), methodType);
                if (!isTokenValid)
                {
                    return await Task.FromResult<HttpResponseMessage>(
                           request.CreateResponse(HttpStatusCode.Unauthorized));
                }
            }
           
            return await base.SendAsync(request, cancellationToken).ContinueWith(
                        task =>
                        {                            
                            ResponseHandler(task);                          
                            var response = task.Result;
                            return response;
                        }
            );
        }

        public void ResponseHandler(Task<HttpResponseMessage> task)
        {
            var headers = task.Result.ToString();
            var body = task.Result.Content.ReadAsStringAsync().Result;

            var fullResponse = headers + "\n" + body;
        }
    }
   
}