using MongoMagno.Models;
using MongoMagno.Services.Commands;

namespace MongoMagno.Services.Routing
{
    public class CommandRouter
    {
        private readonly CommandRouteContainer _routeContainer;

        public CommandRouter(CommandRouteContainer routeContainer)
        {   
            _routeContainer = routeContainer;
        }

        public RouteMatchResult FindRouteForCommand(ClientCommand command)
        {
            var result = _routeContainer.FindRouteForCommand(command.CommandText);
            return result;
        }
    }
}