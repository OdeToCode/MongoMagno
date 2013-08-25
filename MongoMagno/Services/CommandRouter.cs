using System;
using MongoMagno.Models;

namespace MongoMagno.Services
{
    public class CommandRouter
    {
        private readonly CommandRouteTable _routeTable;

        public CommandRouter(CommandRouteTable routeTable)
        {   
            _routeTable = routeTable;
        }

        public RouteMatchResult GetRouteMatch(ClientCommand command)
        {
            var result = _routeTable.Match(command.CommandText);
            return result;
        }
    }
}