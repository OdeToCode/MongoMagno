using System.Collections;
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
        
        public IEnumerable Find(string databaseName, string collectionName, BsonDocument query)
        {
            var db = _server.GetDatabase(databaseName);
            var collection = db.GetCollection(collectionName);
            //var cussor = collection.Find(QueryDocument.Create(""));
            //cussor.Options.            
            return null;

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
    
        MongoServer _server;
    }
}