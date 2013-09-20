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
        private readonly IMongoDb _db;
        private readonly IJavaScriptMachine _vm;
        private readonly InterpretiveCommandMap _commandMap;

        public InterpretiveExecutor(IMongoDb db, IJavaScriptMachine vm)
        {
            _db = db;
            _vm = vm;
            _commandMap = new InterpretiveCommandMap(_db);
        }

        public ExecutionResult Execute(ClientCommand command)
        {
            InitializeEnvironment(command);
            var result = new ExecutionResult();
            result.ParsedCommand = ParseCommand(command);
            foreach (var option in result.ParsedCommand.Operators)
            {
                var executor = _commandMap.GetExecutorFor(option.Name);
                result.Stuff = executor.Execute(option);            
            }
            return result;
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

        void InitializeEnvironment(ClientCommand command)
        {
            var collections = _db.GetCollections(command.Database).ToArray();
            _vm.CreateEnvironment(collections);
        }

        public void Dispose()
        {
            _vm.Dispose();
        }
    }
}