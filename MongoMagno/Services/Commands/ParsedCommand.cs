using System.Collections.Generic;

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
}