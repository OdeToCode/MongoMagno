using System;
using MongoMagno.Models;

namespace MongoMagno.Services.Commands
{
    public interface ICommandExecutor : IDisposable
    {
        ExecutionResult Execute(ClientCommand command);
    }
}