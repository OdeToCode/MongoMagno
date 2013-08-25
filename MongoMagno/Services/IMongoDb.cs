using System.Collections.Generic;

namespace MongoMagno.Services
{
    public interface IMongoDb
    {
        void Connect(string server);
        IEnumerable<string> GetDatabaseNames();
        IEnumerable<string> GetCollections(string database);
    }
}