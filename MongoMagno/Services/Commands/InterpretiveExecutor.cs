using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using MongoDB.Bson;
using MongoMagno.Models;
using MongoMagno.Services.JsVm;
using MongoMagno.Services.Mongo;
using Newtonsoft.Json;

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
            var parsed = ExecuteCommand(command);

            return null;
        }

        private ParsedCommand ExecuteCommand(ClientCommand command)
        {
            dynamic result =  _vm.Evaluate(command.CommandText);
            var parsed = new ParsedCommand();
            for (var i = 0; i < result.captures.length; i++)
            {
                var option = new CommandOperator();
                option.Name = result.captures[i].name;
                var json = JsonConvert.SerializeObject(result.captures[i].args);
                option.Arguments = BsonDocument.Parse(json);
            }
            return parsed;
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