using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace commentsApiAspNet.Domain.Interfaces
{
    public interface ICSVService
    {
        byte[] ConvertToCSV(IEnumerable<Comment> commentList);
    }
}