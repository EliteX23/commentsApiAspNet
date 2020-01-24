using commentsApiAspNet.Domain;
using commentsApiAspNet.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace commentsApiAspNet.Infrastructure.Services
{
    public class CSVService : ICSVService
    {
        const string separator = ",";
        //По умолчанию считаем, что разделитель запятая
        public byte[] ConvertToCSV(IEnumerable<Comment> commentList)
        {
            var fields = new string[] { "id,text,date_create,update_on,created_on,author,topic" };
            string header = String.Join(separator, fields);

            StringBuilder csvdata = new StringBuilder();
            csvdata.AppendLine(header);

            foreach (var o in commentList)
                csvdata.AppendLine($"{o.Id},{o.Text},{o.DateCreate},{o.UpdatedOn},{o.CreatedOn},{o.Author},{o.Topic}");
            return Encoding.UTF8.GetBytes(csvdata.ToString()); 
        }

    }
}