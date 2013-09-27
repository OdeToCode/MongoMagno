using MongoDB.Bson;
using MongoMagno.Services.Mongo;


namespace MongoMagno.Services.Commands
{   
    public class MongoDbResults
    {
        public ParsedCommand ParsedCommands { get; set; }
        public IMongoDbCursor Cursor { get; set; }
        public string Database { get; set; }
        public string Server { get; set; }
        public string Collection { get; set; }          
    }
}