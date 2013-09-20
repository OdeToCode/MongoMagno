using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoMagno.Services.Mongo
{
    public class MongoDb : IMongoDb
    {    
        public void Connect(string server)
        {
            var clientSettings = new MongoClientSettings();
            clientSettings.Server = new MongoServerAddress(server);
            
            var client = new MongoClient(clientSettings);
            _server = client.GetServer();
        }     

        public IMongoDbCursor Find(BsonDocument query)
        {            
            var cursor = _collection.FindAs<BsonDocument>(new QueryDocument(query));
            return new MongoDbCursor(cursor);
        }

        public IEnumerable<string> GetDatabaseNames()
        {
            return _server.GetDatabaseNames();
        }

        public IEnumerable<string> GetCollections(string database)
        {
            var db = _server.GetDatabase(database);
            return db.GetCollectionNames();
        }

        public void SetCurrentCollection(string collectionName)
        {
            _collection = _database.GetCollection(collectionName);
        }

        public void SetCurrentDatabase(string databaseName)
        {
            _database = _server.GetDatabase(databaseName);
        }

        MongoDatabase _database;
        MongoCollection _collection;
        MongoServer _server;
    }
}