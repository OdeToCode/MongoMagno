using System.Collections.Generic;
using System.Web.Http;
using AttributeRouting.Web.Http;
using MongoDB.Driver;

namespace MongoMagno.Controllers
{
    public class ServerController : ApiController
    {     
        [GET("api/server/{server}")]
        public IEnumerable<string> Get(string server, string username = null, string password = null)
        {
            var clientSettings = new MongoClientSettings();
            clientSettings.Server = new MongoServerAddress(server);
            var client = new MongoClient(clientSettings);
            var dbServer = client.GetServer();
            return dbServer.GetDatabaseNames();
        }       
    }
}