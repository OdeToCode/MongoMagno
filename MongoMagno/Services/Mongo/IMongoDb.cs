using System.Collections.Generic;

namespace MongoMagno.Services.Mongo
{
    public interface IMongoDb 
    {
        void Connect(string server);
        IEnumerable<string> GetDatabaseNames();
        IEnumerable<string> GetCollections(string database);
    }
}