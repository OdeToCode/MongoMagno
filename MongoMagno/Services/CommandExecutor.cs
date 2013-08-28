using MongoMagno.Models;
using MongoMagno.Services.JsVm;

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
            var vm = new JavaScriptMachine();
        }
    }
}