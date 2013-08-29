using System;
using MongoMagno.Services.Commands;
using MongoMagno.Services.JsVm;

namespace MongoMagno.Tests.Fakes
{
    public class FakeExecutorResolver : IExecutorResolver
    {
        public ICommandExecutor GetInstance(Type type)
        {
            if (type == typeof (InterpretiveExecutor))
            {
                return new InterpretiveExecutor(new FakeMongoDb(), new JavaScriptMachine());
            }

            return null;
        }
    }
}