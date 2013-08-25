using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Xunit;

namespace MongoMagno.Tests.MongoExperiments
{
    public class Commands
    {
        //[Fact]
        public void CanSendCommandAsJsonToMongo()
        {
            var client = new MongoClient();
            var server = client.GetServer();
            var db = server.GetDatabase("juniperdb");


            var collection = db.GetCollection("bundles");

            var rawQuery = "{}";
            var query = new QueryDocument(BsonDocument.Parse(rawQuery));
            var cursor = collection.Find(query);
           
            Assert.NotNull(cursor);
        }
    }
}