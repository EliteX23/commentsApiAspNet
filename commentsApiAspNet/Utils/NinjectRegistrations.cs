using commentsApiAspNet.Domain.Interfaces;
using commentsApiAspNet.Infrastructure.Repositories;
using commentsApiAspNet.Infrastructure.Services;
using MongoDB.Driver;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace commentsApiAspNet.Utils
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            //возможно, неправильно инициализировать именно тут коннект
            string conStr = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString;
            var mongoUrl = new MongoUrl(conStr);
            var mongoCon = new MongoClient(mongoUrl);
            var database = mongoCon.GetDatabase(mongoUrl.DatabaseName);


            Bind<IMongoDatabase>().ToConstant(database);
            Bind<ICommentRepository>().To<CommentRepository>();
            Bind<IComment>().To<CommentService>();

        }

    }
}