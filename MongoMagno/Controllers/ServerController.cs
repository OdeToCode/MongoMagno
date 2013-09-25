using System;
using System.Collections.Generic;
using System.Web.Http;
using AttributeRouting.Web.Http;
using MongoMagno.Models;
using MongoMagno.Services.Mongo;
using MongoMagno.Services.Routing;

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

        [POST("api/server")]
        public ServerResponse Execute(ClientCommand command)
        {
            var route = _router.FindRouteForCommand(command);
            using (var executor = route.Executor)
            {
                var dbResults = executor.Execute(command);
                
                var response = new ServerResponse();
                response.Data = dbResults.Serialize();
                return response;
            }
        }          
    }
}