using MongoDB.Bson;
using Newtonsoft.Json;

namespace MongoMagno.Services.Commands
{
    public class CommandOperator
    {
        public CommandOperator(string name, dynamic arguments)
        {            
            var json = JsonConvert.SerializeObject(arguments);
            Arguments = BsonDocument.Parse(json);
            Name = name;
        }

        public string Name { get; protected set; }
        public BsonDocument Arguments { get; protected set; }
    }
}