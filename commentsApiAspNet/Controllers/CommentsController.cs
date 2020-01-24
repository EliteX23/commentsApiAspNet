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
using commentsApiAspNet.Domain;
using commentsApiAspNet.Domain.Interfaces;
using commentsApiAspNet.Middlewares;
using Microsoft.AspNet.OData.Routing;
using Microsoft.Data.OData;

namespace commentsApiAspNet.Controllers
{
   
    public class CommentsController : ODataController
    {
        private readonly IComment _commentService;
        public CommentsController(IComment commentService)
        {
            _commentService = commentService;

        }
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();
       
        // GET: odata/Comments
        [EnableQuery]
        public async Task<IHttpActionResult> GetComments(ODataQueryOptions<Comment> queryOptions)
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
            var commentList = await _commentService.GetList();
            return Ok(commentList);

        }

        // GET: odata/Comments(5)
        public async Task<IHttpActionResult> GetComment([FromODataUri] string key, ODataQueryOptions<Comment> queryOptions)
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
            var comment = await _commentService.GetComment(key);
            return Ok(comment);
        }

        // PUT: odata/Comments(5)
        public async Task<IHttpActionResult> Put([FromODataUri] string key, Delta<Comment> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var oldComment = await _commentService.GetComment(key);
            delta.Put(oldComment);
            await _commentService.Update(key, oldComment);
            return Updated(oldComment);

        }

        // POST: odata/Comments
        public async Task<IHttpActionResult> Post(Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _commentService.Save(comment);
            return Created(comment);

        }

        // PATCH: odata/Comments(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] string key, Delta<Comment> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _commentService.GetComment(key);
            delta.Patch(comment);
            await _commentService.Update(key, comment);

            return Updated(comment);

        }

        // DELETE: odata/Comments(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] string key)
        {
            await _commentService.Delete(key);
            return StatusCode(HttpStatusCode.NoContent);

        }

       
    }
}
