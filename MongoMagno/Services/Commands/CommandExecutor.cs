using System;
using MongoMagno.Models;

namespace MongoMagno.Services.Commands
{
    public interface ICommandExecutor : IDisposable
    {
        CommandResult Execute(ClientCommand command);
    }
}