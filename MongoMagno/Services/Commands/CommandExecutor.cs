using System;
using MongoMagno.Models;

namespace MongoMagno.Services.Commands
{
    public interface ICommandExecutor : IDisposable
    {
        SomethingResult Execute(ClientCommand command);
    }
}