using MongoMagno.Models;

namespace MongoMagno.Services
{
    public interface CommandExecutor
    {
        void Execute(ClientCommand command, RouteMatchResult routeMatch);
    }
    
    public class InterpretiveExecutor : CommandExecutor
    {
        public void Execute(ClientCommand command, RouteMatchResult routeMatch)
        {
            throw new System.NotImplementedException();
        }
    }
}