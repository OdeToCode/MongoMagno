using System.Collections.Generic;
using MongoDB.Bson;

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
        public string Name { get; set; }
        public BsonDocument Arguments { get; set; }
    }
}