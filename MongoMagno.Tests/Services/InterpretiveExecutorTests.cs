using MongoMagno.Exceptions;
using MongoMagno.Models;
using MongoMagno.Services.Commands;
using MongoMagno.Services.JsVm;
using MongoMagno.Tests.Fakes;
using Xunit;

namespace MongoMagno.Tests.Services
{  
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

            Assert.Equal(1, result.ParsedCommands.Operators.Count);
            Assert.Equal("find", result.ParsedCommands.Operators[0].Name);
            Assert.Equal(3, result.ParsedCommands.Operators[0].Arguments["0"].AsBsonDocument["x"].AsInt32);
            Assert.Equal(1, result.ParsedCommands.Operators[0].Arguments["1"].AsBsonDocument["id"].AsInt32);
        }
   
        [Fact]
        public void Throws_When_Syntax_Is_Bad()
        {
            var command = new ClientCommand
            {
                Server = "localhost",
                Database = "test",
                CommandText = "db.collection_a.blarg()"
            };

            var executor = new InterpretiveExecutor(_db, _vm);

            Assert.Throws<ExecutionException>(() => executor.Execute(command));
        }
    }
}