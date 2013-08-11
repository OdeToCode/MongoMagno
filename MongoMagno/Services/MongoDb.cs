using System.Collections.Generic;
using MongoDB.Driver;

namespace MongoMagno.Services
{
    public class MongoDb
    {
        public MongoDb(string server, string username = null, string password = null)
        {
            var clientSettings = new MongoClientSettings();
            clientSettings.Server = new MongoServerAddress(server);
            var client = new MongoClient(clientSettings);
            _server = client.GetServer();
        }

        public IEnumerable<string> GetDatabaseNames()
        {
            return _server.GetDatabaseNames();
        }

        MongoServer _server;
    }
}