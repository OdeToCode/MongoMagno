using System.Collections.Generic;
using System.Web.Http;
using AttributeRouting.Web.Http;
using MongoMagno.Services;

namespace MongoMagno.Controllers
{
    public class ServerController : ApiController
    {         
        [GET("api/server/{server}")]
        public IEnumerable<string> GetDatabases(string server)
        {
            var db = new MongoDb(server);
            return db.GetDatabaseNames();
        }

        [GET("api/server/{server}/{database}")]
        public IEnumerable<string> GetCollections(string server, string database)
        {
            var db = new MongoDb(server);
            return db.GetCollections(database);
        }     
    }
}