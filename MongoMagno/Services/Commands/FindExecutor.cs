using MongoDB.Driver;
using MongoMagno.Models;
using MongoMagno.Services.Mongo;

namespace MongoMagno.Services.Commands
{
    public class FindExecutor : IOperatorExecutor
    {
        private readonly IMongoDb _db;

        public FindExecutor(IMongoDb db)
        {
            _db = db;
        }


        public SomethingResult Execute(CommandOperator op)
        {
            var cursor = _db.Find(op.Arguments);
            return new SomethingResult() {Command = "find"};
        }
    }
}