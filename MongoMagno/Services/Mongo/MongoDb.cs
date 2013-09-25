using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoMagno.Services.Mongo
{
    public class MongoDb : IMongoDb
    {    
       
        public void Connect(string server)
        {
           Check.ArgNotNull(server, "server");

            var clientSettings = new MongoClientSettings {Server = new MongoServerAddress(server)};
            var client = new MongoClient(clientSettings);
            _server = client.GetServer();
        }     

        public IMongoDbCursor Find(BsonDocument query)
        {
            Check.ArgNotNull(query, "query");
            Check.NotNull(_collection, "_collection");

            var cursor = _collection.FindAs<BsonDocument>(new QueryDocument(query));
            return new MongoDbCursor(cursor);
        }

        public IEnumerable<string> GetDatabaseNames()
        {
            Check.NotNull(_server, "_server");

            return _server.GetDatabaseNames();
        }

        public IEnumerable<string> GetCollections(string database)
        {           
            Check.ArgNotNull(database, "database");
            Check.NotNull(_server, "server");

            var db = _server.GetDatabase(database);
            return db.GetCollectionNames();
        }

        public void SetCurrentCollection(string collectionName)
        {
            Check.ArgNotNull(collectionName, "collectionName");
            Check.NotNull(_server, "_server");;

            _collection = _database.GetCollection(collectionName);
        }

        public void SetCurrentDatabase(string databaseName)
        {
            Check.ArgNotNull(databaseName, "databaseName");
            Check.NotNull(_server, "_server");

            _database = _server.GetDatabase(databaseName);
        }

        MongoDatabase _database;
        MongoCollection _collection;
        MongoServer _server;
    }
}