using System;
using MongoMagno.Models;

namespace MongoMagno.Services
{
    public class CommandRouter
    {
        private readonly ClientCommand _command;
        private readonly CommandRouteTable _routeTable;

        public CommandRouter(CommandRouteTable routeTable, ClientCommand command)
        {
            _command = command;
            _routeTable = routeTable;
        }

        public RouteMatchResult GetRouteMatch()
        {
            var result = _routeTable.Match(_command.CommandText);
            return result;
        }
    }
}