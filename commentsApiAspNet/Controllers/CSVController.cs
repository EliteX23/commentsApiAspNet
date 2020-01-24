using commentsApiAspNet.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace commentsApiAspNet.Controllers
{
    public class CSVController : ApiController
    {
        private readonly IComment _commentService;
       
        private readonly ICSVService _csvService;
        public CSVController(IComment commentService,ICSVService csvService)
        {
            _commentService = commentService;
            _csvService = csvService;
        }

        [System.Web.Http.HttpGet]
        public  async Task<HttpResponseMessage> Export()
        {

            var allComments =await _commentService.GetList();
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(new MemoryStream(_csvService.ConvertToCSV(allComments)));

            var fileName = $"comments-{DateTime.Now.ToFileTimeUtc()}.csv";            
            result.Content.Headers.ContentDisposition = new
                ContentDispositionHeaderValue("attachment")
            {
                FileName = fileName
            };
            return result;
        }
    }
}
