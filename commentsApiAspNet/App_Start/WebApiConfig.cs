using commentsApiAspNet.Domain;
using commentsApiAspNet.Domain.Interfaces;
using commentsApiAspNet.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using System.Web.Http.OData.Formatter;

namespace commentsApiAspNet
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы веб-API

            // Маршруты веб-API
            config.MapHttpAttributeRoutes();
            
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Comment>("Comments");
            
            var session = (IToken)config.DependencyResolver.GetService(typeof(IToken));           
            var middleware = new TokenMiddleware(session);        
            config.MessageHandlers.Add(middleware);
           
            //config.MessageHandlers.Add
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
           
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
      
    }
   
}
