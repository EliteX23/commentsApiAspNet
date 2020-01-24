using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace commentsApiAspNet.Utils
{
    public class CSVActionResult : FileResult
    {
        private string _fileName;
        private byte[] _fileBytes;

        public CSVActionResult(string fileName, byte[] fileBytes)
            : base("text/csv")
        {
            _fileName = fileName;
            _fileBytes = fileBytes;
        }
        //public void ExecuteResult(ControllerContext context)
        //{
        //    context.HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "X-File-Name");
        //    context.HttpContext.Response.Headers.Add("X-File-Name", _fileName);
        //    context.HttpContext.Response.Headers.Add("Content-Type", "text/csv");
        //    new MemoryStream(_fileBytes).CopyToAsync(context.HttpContext.Response.Output).Wait();
        //    base.ExecuteResult(context);
        //    return
        //}

        protected override void WriteFile(HttpResponseBase response)
        {
            throw new NotImplementedException();
        }
    }
}