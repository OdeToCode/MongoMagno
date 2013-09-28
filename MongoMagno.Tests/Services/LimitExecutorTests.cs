using System;
using MongoDB.Bson;
using MongoMagno.Exceptions;
using MongoMagno.Services.Commands;
using MongoMagno.Tests.Fakes;
using Xunit;

namespace MongoMagno.Tests.Services
{
    public class LimitExecutorTests
    {
        [Fact]
        public void Limit_Executor_Limits_Query()
        {
            var op = new CommandOperator("limit", new { Value = 22});
            var limiter = new LimitExecutor();
            var cursor = new FakeMongoCursor();
            var result = new MongoDbResults { Cursor = cursor };

            limiter.Apply(op, result);

            Assert.Equal(22, cursor.CurrentLimit);
        }

        [Fact]
        public void Limit_Executor_Throws_When_Invalid_Argument()
        {            
            var op = new CommandOperator("limit", new { Value = "xyz"});
            var limiter = new LimitExecutor();
            var result = new MongoDbResults();
            result.Cursor = new FakeMongoCursor();

            Assert.Throws<InvalidQueryArgumentException>(() => limiter.Apply(op, result));
        }
    }
}