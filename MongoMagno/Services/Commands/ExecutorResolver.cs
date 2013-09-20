using System;
using StructureMap;

namespace MongoMagno.Services.Commands
{
    public class ExecutorResolver : IExecutorResolver
    {
        public ICommandExecutor GetInstance(Type type)
        {
            return (ICommandExecutor)ObjectFactory.GetInstance(type);
        }
    }
}