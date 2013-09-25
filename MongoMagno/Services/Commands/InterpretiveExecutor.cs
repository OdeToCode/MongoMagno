using System.Linq;
using Microsoft.ClearScript;
using MongoMagno.Exceptions;
using MongoMagno.Models;
using MongoMagno.Services.JsVm;
using MongoMagno.Services.Mongo;

namespace MongoMagno.Services.Commands
{
    public class InterpretiveExecutor : ICommandExecutor
    {       
        public InterpretiveExecutor(IMongoDb db, IJavaScriptMachine vm)
        {
            _db = db;
            _vm = vm;
            _commandMap = new InterpretiveCommandMap(_db);
        }

        public MongoDbResults Execute(ClientCommand command)
        {
            InitializeDb(command);
            InitializeJvm(command);
            return ExecuteCommand(command);
        }

        private void InitializeDb(ClientCommand command)
        {
            _db.Connect(command.Server);
            _db.SetCurrentDatabase(command.Database);
        }

        MongoDbResults ExecuteCommand(ClientCommand command)
        {
            var result = Parse(command);            
            return Execute(result);
        }

        private MongoDbResults Execute(MongoDbResults result)
        {
            foreach (var option in result.ParsedCommands.Operators)
            {
                var executor = _commandMap.GetExecutorFor(option.Name);
                result = executor.Apply(option, result);
            }
            return result;
        }

        private MongoDbResults Parse(ClientCommand command)
        {
            var result = new MongoDbResults();
            result.ParsedCommands = ParseCommand(command);
            result.Server = command.Server;
            result.Database = command.Database;
            result.Collection = result.ParsedCommands.Collection;
            _db.SetCurrentCollection(result.ParsedCommands.Collection);
            return result;
        }

        void InitializeJvm(ClientCommand command)
        {           
            var collections = _db.GetCollections(command.Database).ToArray();
            _vm.CreateEnvironment(collections);
        }

        ParsedCommand ParseCommand(ClientCommand command)
        {
            dynamic result = EvaluateCommand(command);
            
            var parsed = new ParsedCommand();
            parsed.Collection = result.collectionName;
            for (var i = 0; i < result.captures.length; i++)
            {
                var option = new CommandOperator(result.captures[i].name, result.captures[i].args);
                parsed.Operators.Add(option);
            }

            return parsed;
        }

        dynamic EvaluateCommand(ClientCommand command)
        {
            try
            {
                return _vm.Evaluate(command.CommandText);
            }
            catch (ScriptEngineException ex)
            {
                throw new ExecutionException(ex);
            }
        }    

        public void Dispose()
        {
            _vm.Dispose();
        }

        readonly IMongoDb _db;
        readonly IJavaScriptMachine _vm;
        readonly InterpretiveCommandMap _commandMap;
    }
}