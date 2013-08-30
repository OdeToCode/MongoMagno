using System.Collections.Generic;
using MongoMagno.Services.Mongo;

namespace MongoMagno.Tests.Fakes
{
    public class FakeMongoDb : IMongoDb
    {
        public void Connect(string server)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<string> GetDatabaseNames()
        {
            return new[] {"dba", "dbb", "dbc"};
        }

        public IEnumerable<string> GetCollections(string database)
        {
            return new[] {"collection_a", "collection_b"};
        }

        public string Server;
        public string[] Databases = {"dba", "dbb", "dbc"};
        public string[] Collections = {"collection_a", "collection_b"};
    }
}