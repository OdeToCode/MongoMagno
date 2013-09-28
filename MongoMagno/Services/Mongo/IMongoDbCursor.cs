using System.Collections;

namespace MongoMagno.Services.Mongo
{
    public interface IMongoDbCursor : IEnumerable
    {
        IMongoDbCursor Limit(int limit);
    }
}