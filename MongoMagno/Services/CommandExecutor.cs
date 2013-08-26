using MongoMagno.Models;

namespace MongoMagno.Services
{
    public interface CommandExecutor
    {
        void Execute(ClientCommand command, RouteMatchResult routeMatch);
    }
    
    [CommandMatch(@"^db.(?<collection>\w*).find\(")]
    public class FindExecutor : CommandExecutor
    {
        private readonly IMongoDb _db;

        public FindExecutor(IMongoDb db)
        {
            _db = db;
        }

        public void Execute(ClientCommand command, RouteMatchResult routeMatch)
        {
            var collectionName = routeMatch.Tokens["collection"];
            //_db.Query(collectionName, )
            

        }
    }
}