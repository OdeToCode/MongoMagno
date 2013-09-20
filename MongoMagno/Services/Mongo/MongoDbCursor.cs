using MongoDB.Driver;

namespace MongoMagno.Services.Mongo
{
    public class MongoDbCursor : IMongoDbCursor
    {
        private readonly MongoCursor _cursor;

        public MongoDbCursor(MongoCursor cursor)
        {
            _cursor = cursor;
        }
    }
}