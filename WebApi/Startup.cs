using Microsoft.Owin;
using Newtonsoft.Json;
using Owin;
using System.Net.Http.Headers;
using System.Web.Http;
using NLog;
using Unity.AspNet.WebApi;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
[assembly: OwinStartup(typeof(Payoneer.Payoneer.Hotels.WebApi.Startup))]

namespace Payoneer.Payoneer.Hotels.WebApi
{

    public class Startup
    {

        public void Configuration(IAppBuilder app)

        {
            //ILogger log = LogManager.GetCurrentClassLogger();
            LogManager.GetCurrentClassLogger().Info("Service is starting...");

            var httpConfiguration = new HttpConfiguration();
            httpConfiguration.MapHttpAttributeRoutes();
            httpConfiguration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            // To ensure that test calls from browsers get json by default. XML is still supported by setting
            // Content-Type: application/xml
            httpConfiguration.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            app.UseWebApi(httpConfiguration);

            var resolver = new UnityDependencyResolver(UnityConfig.Container);
            httpConfiguration.DependencyResolver = resolver;

            //add any middleware with app.Use()
            SwaggerConfig.Register(httpConfiguration);
            httpConfiguration.EnsureInitialized();

            //var cacheUtil = new CacheControllerUtil();
            //cacheUtil.RegisterAllCaches();

            LogManager.GetCurrentClassLogger().Info("Service has started.");
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
