using System.Collections.Generic;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace MongoMagno.Services.Commands
{
    public class ParsedCommand
    {
        public ParsedCommand()
        {
            Operators = new List<CommandOperator>();
        }

        public string Collection { get; set; }
        public IList<CommandOperator> Operators { get; set; }
    }

    public class CommandOperator
    {
        public CommandOperator(string name, object arguments)
        {
            Name = name;
            var json = JsonConvert.SerializeObject(arguments);
            Arguments = BsonDocument.Parse(json);
        }

        public string Name { get; protected set; }
        public BsonDocument Arguments { get; protected set; }
    }
}