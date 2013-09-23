using System;
using MongoMagno.Services.Mongo;
using Xunit;

namespace MongoMagno.Tests.Services
{
    public class MongoDbTests
    {
        [Fact]
        public void Real_MongoDb_Throws_If_Server_Is_Null()
        {
            var db = new MongoDb();

            Assert.Throws<ArgumentNullException>(() => db.Connect(null));
        }
    }
}