using MongoMagno.Models;
using MongoMagno.Services.Commands;
using MongoMagno.Services.Routing;
using MongoMagno.Tests.Fakes;
using Xunit;

namespace MongoMagno.Tests.Services
{
    public class CommandRouterTests
    {
        private CommandRouteContainer _routeContainer;

        public CommandRouterTests()
        {
            _routeContainer = new CommandRouteContainer(new FakeExecutorResolver());
        }

        [Fact]
        public void Routes_Find_A_Query()
        {
            var router = new CommandRouter(_routeContainer);
            var result = router.FindRouteForCommand(new ClientCommand { CommandText = "db.foocollection.find({})" });

            Assert.Equal(typeof(InterpretiveExecutor), result.Executor.GetType());
        }
    }
}