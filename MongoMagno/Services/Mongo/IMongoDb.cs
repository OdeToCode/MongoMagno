using System.Collections;
using System.Collections.Generic;
using MongoDB.Bson;

namespace MongoMagno.Services.Mongo
{
    public interface IMongoDb
    {        
        void Connect(string server);
        IEnumerable<string> GetDatabaseNames();
        IEnumerable<string> GetCollections(string database);
        void SetCurrentDatabase(string databaseName);
        void SetCurrentCollection(string collectionName);
        IMongoDbCursor Find(BsonDocument query);
    }
}