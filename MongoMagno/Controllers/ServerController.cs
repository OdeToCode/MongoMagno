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
        private readonly CommandRouter _router;

        public ServerController(IMongoDb db, CommandRouter router)
        {
            _db = db;
            _router = router;
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
        public RouteMatchResult Execute(string server, string database, ClientCommand command)
        {
            var route = _router.GetRouteMatch(command);
            return route;
        }          
    }
}