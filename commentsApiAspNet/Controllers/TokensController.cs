using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using System.Web.Http.OData.Routing;
using commentsApiAspNet.Domain.Core;
using commentsApiAspNet.Domain.Interfaces;
using Microsoft.Data.OData;

namespace commentsApiAspNet.Controllers
{

    //весь коньроллер сделан для удобства тестирования
    public class TokensController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();
        private readonly IToken _tokenService;
        public TokensController(IToken tokenService)
        {
            _tokenService = tokenService;
        }

        // GET: odata/Tokens
        public async Task<IHttpActionResult> GetTokens(ODataQueryOptions<Token> queryOptions)
        {
            // validate the query.
            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }
            var tokenList = await _tokenService.GetList();

            return Ok(tokenList);

        }

        // GET: odata/Tokens(5)
        public async Task<IHttpActionResult> GetToken([FromODataUri] string key, ODataQueryOptions<Token> queryOptions)
        {
            // validate the query.
            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            var token = await _tokenService.GetApiKey(key);

            return Ok(token);
        }

        // PUT: odata/Tokens(5)
        public async Task<IHttpActionResult> Put([FromODataUri] string key, Delta<Token> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var token = await _tokenService.GetApiKey(key);
            delta.Put(token);
            await _tokenService.Save(token);
            return Updated(token);
        }

        // POST: odata/Tokens
        public async Task<IHttpActionResult> Post(Token token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _tokenService.Save(token);
            return Created(token);

        }

        // PATCH: odata/Tokens(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] string key, Delta<Token> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var token = await _tokenService.GetApiKey(key);
            delta.Put(token);
            await _tokenService.Save(token);
            return Updated(token);
        }

        // DELETE: odata/Tokens(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] string key)
        {
            await _tokenService.Delete(key);
            return StatusCode(HttpStatusCode.NoContent);

        }
    }
}
