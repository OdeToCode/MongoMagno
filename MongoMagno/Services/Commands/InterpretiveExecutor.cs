using System;
using System.Collections.Generic;
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
            InitializeEnvironment(command);
            var result = ExecuteCommand(command);
            return result;
        }

        private CommandResult ExecuteCommand(ClientCommand command)
        {
            dynamic result =  _vm.Evaluate(command.CommandText);
            //if (result.name == "find")
            //{
            //    return new CommandResult() {Command = "Find"};
            //}
            return null;
        }

        private void InitializeEnvironment(ClientCommand command)
        {
            var collections = _db.GetCollections(command.Database).ToArray();
            _vm.CreateEnvironment(collections);
        }

        public void Dispose()
        {
            _vm.Dispose();
        }
    }

    public class InterpretiveCommandMap
    {
        private readonly IMongoDb _db;

        public InterpretiveCommandMap(IMongoDb db)
        {
            _db = db;
        }

        public InterpretiveCommandMap()
        {
            _map.Add("Find", Find);
        }

        private CommandResult Find(dynamic arg)
        {
            //var result = _db.Find
            return null;
        }

        private Dictionary<string, Func<dynamic, CommandResult>> _map =
            new Dictionary<string, Func<dynamic, CommandResult>>();
    }
}