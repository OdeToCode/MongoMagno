using MongoMagno.Models;
using MongoMagno.Services.Commands;
using MongoMagno.Services.JsVm;
using MongoMagno.Tests.Fakes;
using Xunit;

namespace MongoMagno.Tests.Services
{
    public interface IFoo<T>
    {
        
    }

    public class InterpretiveExecutorTests
    {
        FakeMongoDb _db;
        JavaScriptMachine _vm;

        public InterpretiveExecutorTests()
        {
            _db = new FakeMongoDb();
            _vm = new JavaScriptMachine();             
        }

        [Fact]
        public void Can_Execute_Simple_Query()
        {                       
            var command = new ClientCommand
                {
                    Server = "localhost",
                    Database = "test",
                    CommandText = "db.collection_a.find({x:3}, {id:1})"
                };

            var executor = new InterpretiveExecutor(_db, _vm);
            var result = executor.Execute(command);

            Assert.Equal("Find", result.Command);
        }       
    }
}