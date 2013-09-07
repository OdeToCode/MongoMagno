using System.Collections;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoMagno.Services.Mongo;

namespace MongoMagno.Tests.Fakes
{
    public class FakeMongoDb : IMongoDb
    {
        public void Connect(string server)
        {
            
        }

        public IEnumerable<string> GetDatabaseNames()
        {
            return new[] {"dba", "dbb", "dbc"};
        }

        public IEnumerable<string> GetCollections(string database)
        {
            return new[] {"collection_a", "collection_b"};
        }

        public IEnumerable Find(string database, string collection, BsonDocument document)
        {
            return null;
        }

        public string Server;
        public string[] Databases = {"dba", "dbb", "dbc"};
        public string[] Collections = {"collection_a", "collection_b"};
    }
}