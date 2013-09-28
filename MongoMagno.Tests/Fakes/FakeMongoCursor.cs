using System.Collections;
using MongoMagno.Services.Mongo;

namespace MongoMagno.Tests.Fakes
{
    public class FakeMongoCursor : IMongoDbCursor
    {
        public IEnumerator GetEnumerator()
        {
            return null;
        }

        public IMongoDbCursor Limit(int limit)
        {
            CurrentLimit = limit;
            return this;
        }

        public int CurrentLimit { get; set; }
    }
}