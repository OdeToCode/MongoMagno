using System.Collections.Generic;
using System.Web.Http;
using AttributeRouting.Web.Http;
using MongoMagno.Models;
using MongoMagno.Services;

namespace MongoMagno.Controllers
{
    public class ServerController : ApiController
    {
        private readonly IMongoDb _db;

        public ServerController(IMongoDb db)
        {
            _db = db;
        }      

        [GET("api/server/{server}")]
        public IEnumerable<string> GetDatabases(string server)
        {
            _db.Connect(server);
            return _db.GetDatabaseNames();
        }

        [GET("api/server/{server}/{database}")]
        public IEnumerable<string> GetCollections(string server, string database)
        {
            _db.Connect(server);
            return _db.GetCollections(database);
        }

        [POST("api/server/{server}/{database}")]
        public string Execute(string server, string database, ClientCommand command)
        {
            return "execute";
        }          
    }
}