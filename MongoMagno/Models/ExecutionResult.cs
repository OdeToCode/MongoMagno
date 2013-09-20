using MongoMagno.Services.Commands;

namespace MongoMagno.Models
{
    public class ExecutionResult
    {
        public ParsedCommand ParsedCommand { get; set; }
        public string Stuff { get; set; }
    }
}