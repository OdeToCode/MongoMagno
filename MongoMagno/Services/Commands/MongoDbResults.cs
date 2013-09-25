using System.Collections;
using MongoMagno.Services.Mongo;
using Newtonsoft.Json;

namespace MongoMagno.Services.Commands
{
    public class MongoDbResults
    {
        public ParsedCommand ParsedCommands { get; set; }
        public IMongoDbCursor Cursor { get; set; }
        public string Database { get; set; }
        public string Server { get; set; }
        public string Collection { get; set; }

        public string Serialize()
        {
            var enumerable = Cursor as IEnumerable;
            return JsonConvert.SerializeObject(enumerable);
        }     
    }
}