using MongoMagno.Models;
using MongoMagno.Services.Mongo;

namespace MongoMagno.Services.Commands
{
    public class LimitExecutor : IApplyOperation
    {
        public MongoDbResults Apply(CommandOperator op, MongoDbResults result)
        {

            //result.Cursor.Limit(op.Arguments);
            return result;
        }
    }

    public class FindExecutor : IApplyOperation
    {
        private readonly IMongoDb _db;

        public FindExecutor(IMongoDb db)
        {
            _db = db;
        }


        public MongoDbResults Apply(CommandOperator op, MongoDbResults result)
        {
            result.Cursor = _db.Find(op.Arguments);
            return result;
        }
    }
}