﻿using MongoMagno.Models;
using MongoMagno.Services;
using Xunit;

namespace MongoMagno.Tests.Services
{
    public class CommandRouterTests
    {
        private CommandRouteTable _routeTable;

        public CommandRouterTests()
        {
            _routeTable = new CommandRouteTable();
            _routeTable.Initialize();
        }

        [Fact]
        public void Invalid_Route_Uses_Null_Executor()
        {
            var router = new CommandRouter(_routeTable);
            var result = router.GetRouteMatch(new ClientCommand { CommandText = "blahblah" });

            Assert.Equal(RouteMatchResult.Default, result);
        }

         [Fact]
         public void Routes_Find_A_Query()
         {
             var router = new CommandRouter(_routeTable);
             var result = router.GetRouteMatch(new ClientCommand { CommandText = "db.foocollection.find({})" });             

             Assert.Equal(typeof(InterpretiveExecutor), result.Type);
         }        
    }
}