using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.Web.Http;
using Microsoft.Web.Http.Versioning;

namespace TestProject.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы веб-API

            // Маршруты веб-API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.AddApiVersioning(o =>
                {
                    o.ReportApiVersions = true;
                    o.AssumeDefaultVersionWhenUnspecified = true;
                    o.DefaultApiVersion = new ApiVersion(1, 0);
                    o.ApiVersionReader = new HeaderApiVersionReader("api-version");
                    o.ApiVersionSelector = new CurrentImplementationApiVersionSelector(o);
                }
            );

        }
    }
}
