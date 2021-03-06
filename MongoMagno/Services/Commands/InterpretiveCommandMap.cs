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
            _map = new Dictionary<string, IApplyOperation>();
            _map.Add("find", new FindExecutor(_db));
            _map.Add("limit", new LimitExecutor());
        }

        public IApplyOperation GetExecutorFor(string command)
        {
            if (_map.ContainsKey(command))
            {
                return _map[command];
            }
            throw new CommandNotFoundException(command);
        }

        readonly IMongoDb _db;
        readonly Dictionary<string, IApplyOperation> _map;

    }
}