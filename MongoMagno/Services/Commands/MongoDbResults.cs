using MongoMagno.Services.Mongo;

namespace MongoMagno.Services.Commands
{
    public class MongoDbResults
    {
        public ParsedCommand ParsedCommands { get; set; }
        public IMongoDbCursor Cursor { get; set; }
    }
}