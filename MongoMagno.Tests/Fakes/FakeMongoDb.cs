using MongoMagno.Services.Mongo;

namespace MongoMagno.Tests.Fakes
{
    public class FakeMongoDb : IMongoDb
    {
        public void Connect(string server)
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.IEnumerable<string> GetDatabaseNames()
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.IEnumerable<string> GetCollections(string database)
        {
            throw new System.NotImplementedException();
        }
    }
}