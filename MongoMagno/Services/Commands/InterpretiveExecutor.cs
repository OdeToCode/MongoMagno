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

        public InterpretiveExecutor(IMongoDb db, IJavaScriptMachine vm)
        {
            _db = db;
            _vm = vm;
        }

        public CommandResult Execute(ClientCommand command)
        {
            var collections = _db.GetCollections(command.Database).ToArray();            
            _vm.CreateEnvironment(command.Database, collections);

            return null;
        }

        public void Dispose()
        {
            _vm.Dispose();
        }
    }
}