using MongoMagno.Services.Commands;

namespace MongoMagno.Services.Routing
{
    public class RouteMatchResult
    {
        public RouteMatchResult()
        {
            Tokens = new RouteMatchTokens();
        }

        public ICommandExecutor Executor { get; set; }
        public RouteMatchTokens Tokens { get; set; }       
    }
}