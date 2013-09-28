using System.Collections;
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

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable) _cursor).GetEnumerator();
        }

        public IMongoDbCursor Limit(int limit)
        {
            _cursor.Limit = limit;
            return this;
        }
    }
}