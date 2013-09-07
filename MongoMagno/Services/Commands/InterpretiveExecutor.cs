using System;
using System.Collections.Generic;
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
        private readonly InterpretiveCommandMap _map;

        public InterpretiveExecutor(IMongoDb db, IJavaScriptMachine vm)
        {
            _db = db;
            _vm = vm;
            _map = new InterpretiveCommandMap(this);
        }

        public CommandResult Execute(ClientCommand command)
        {
            InitializeEnvironment(command);
            var parsed = ParseCommand(command);
            foreach (var option in parsed.Operators)
            {
                
            }
            return null;
        }

        private ParsedCommand ParseCommand(ClientCommand command)
        {
            dynamic result =  _vm.Evaluate(command.CommandText);
           
            var parsed = new ParsedCommand();
            parsed.Collection = result.collectionName;            
            
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

  
    public class InterpretiveCommandMap : Dictionary<string, Func<ParsedCommand, CommandResult>>
    {
        private readonly InterpretiveExecutor _executor;

        public InterpretiveCommandMap(InterpretiveExecutor executor)
        {
            _executor = executor;
        }
    }
}