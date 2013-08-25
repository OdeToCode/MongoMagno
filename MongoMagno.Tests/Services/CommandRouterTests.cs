using MongoMagno.Models;
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
            var router = new CommandRouter(_routeTable, new ClientCommand { CommandText = "blahblah" });
            var result = router.GetRouteMatch();

            Assert.Equal(RouteMatchResult.Empty, result);
        }

         [Fact]
         public void Routes_Find_A_Query()
         {
             var router = new CommandRouter(_routeTable, new ClientCommand { CommandText = "db.foocollection.find({})" });
             var result = router.GetRouteMatch();             

             Assert.Equal(typeof(FindExecutor), result.Type);
         }

        [Fact]
        public void Finds_Collection_Name_In_Query()
        {
            var router = new CommandRouter(_routeTable, new ClientCommand { CommandText = "db.foocollection.find({})" });
            var result = router.GetRouteMatch();

            Assert.Equal("foocollection", result.Tokens["collection"]);
        }
    }
}