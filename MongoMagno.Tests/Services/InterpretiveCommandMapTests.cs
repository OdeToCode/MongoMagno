using MongoMagno.Exceptions;
using MongoMagno.Services.Commands;
using MongoMagno.Tests.Fakes;
using Xunit;

namespace MongoMagno.Tests.Services
{
    public class InterpretiveCommandMapTests
    {
        [Fact]
        public void Throws_When_Command_Not_Registered()
        {
            var map = new InterpretiveCommandMap(new FakeMongoDb());

            Assert.Throws<CommandNotFoundException>(() => map.GetExecutorFor("blarg"));
        }
    }
}
