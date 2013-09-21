using MongoMagno.Models;
using MongoMagno.Services.Mongo;

namespace MongoMagno.Services.Commands
{
    public class LimitExecutor : IOperatorExecutor
    {        
        public string Execute(CommandOperator op)
        {
            return "temp";
        }
    }

    public class FindExecutor : IOperatorExecutor
    {
        private readonly IMongoDb _db;

        public FindExecutor(IMongoDb db)
        {
            _db = db;
        }


        public string Execute(CommandOperator op)
        {
            var cursor = _db.Find(op.Arguments);
            return "temp";
        }
    }
}