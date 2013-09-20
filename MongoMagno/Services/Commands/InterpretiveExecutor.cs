using System.Linq;
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

        public SomethingResult Execute(ClientCommand command)
        {
            InitializeEnvironment(command);
            var parsed = ParseCommand(command);
            foreach (var option in parsed.Operators)
            {
                var executor = _commandMap.GetExecutorFor(option.Name);
                var result = executor.Execute(option);
                return result;
            }
            return null;

        }
     
        ParsedCommand ParseCommand(ClientCommand command)
        {
            dynamic result =  _vm.Evaluate(command.CommandText);
           
            var parsed = new ParsedCommand();
            parsed.Collection = result.collectionName;            
            
            for (var i = 0; i < result.captures.length; i++)
            {
                var option = new CommandOperator(result.captures[i].name, result.captures[i].args);                
                parsed.Operators.Add(option);
            }
            return parsed;
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