using System.Web.Http;
using System.Web.Http.ValueProviders;
using AttributeRouting.Web.Http.WebHost;
using MongoMagno.Binding;

namespace MongoMagno
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpAttributeRoutes();
            config.Services.Add(typeof(ValueProviderFactory), new HeaderValueProvider<string>.Factory());
        }
    }
}
