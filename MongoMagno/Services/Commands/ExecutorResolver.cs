using System;
using StructureMap;

namespace MongoMagno.Services.Commands
{
    public interface IExecutorResolver
    {
        ICommandExecutor GetInstance(Type type);
    }

    public class ExecutorResolver : IExecutorResolver
    {
        public ICommandExecutor GetInstance(Type type)
        {
            return (ICommandExecutor)ObjectFactory.GetInstance(type);
        }
    }
}