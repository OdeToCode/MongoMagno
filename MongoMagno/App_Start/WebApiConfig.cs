using System.Web.Http;
using System.Web.Http.ValueProviders;
using AttributeRouting.Web.Http.WebHost;
using MongoMagno.Binding;
using MongoMagno.Services.Mongo;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MongoMagno
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpAttributeRoutes();
            config.Services.Add(typeof(ValueProviderFactory), new HeaderValueProvider<string>.Factory());
            
            var formatters = GlobalConfiguration.Configuration.Formatters;
            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;

            jsonFormatter.SerializerSettings.Converters.Add(new MongoCursorSerializer());
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
