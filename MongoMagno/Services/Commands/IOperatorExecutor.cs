using MongoMagno.Models;

namespace MongoMagno.Services.Commands
{
    public interface IOperatorExecutor
    {
        string Execute(CommandOperator  op);
    }
}