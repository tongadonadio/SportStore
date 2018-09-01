using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using SportStore.API.Unity;
using SportStore.BusinessLogic.V1;
using SportStore.BusinessLogic;
using SportStore.Repository.Entity;

namespace SportStore.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var businessLogic = new SportStoreBusinessLogic<SportStoreRepository>();

            // Web API configuration and services

            // CORS
            config.EnableCors();

            // Web API Dependency Injection
            var container = new UnityContainer();
            container.RegisterInstance<ISportStoreBusinessLogic>(businessLogic);
            config.DependencyResolver = new UnityResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
