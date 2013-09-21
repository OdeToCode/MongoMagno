using System.Collections.Generic;
using MongoMagno.Exceptions;
using MongoMagno.Services.Mongo;

namespace MongoMagno.Services.Commands
{
    public class InterpretiveCommandMap
    {
        public InterpretiveCommandMap(IMongoDb db)
        {
            _db = db;
            _map = new Dictionary<string, IOperatorExecutor>();
            _map.Add("find", new FindExecutor(_db));
            _map.Add("limit", new LimitExecutor());
        }

        public IOperatorExecutor GetExecutorFor(string command)
        {
            if (_map.ContainsKey(command))
            {
                return _map[command];
            }
            throw new CommandNotFoundException(command);
        }

        readonly IMongoDb _db;
        readonly Dictionary<string, IOperatorExecutor> _map;

    }
}