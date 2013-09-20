using System;

namespace MongoMagno.Services.Commands
{
    public interface IExecutorResolver
    {
        ICommandExecutor GetInstance(Type type);
    }
}