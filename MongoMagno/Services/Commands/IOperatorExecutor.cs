using MongoMagno.Models;

namespace MongoMagno.Services.Commands
{
    public interface IOperatorExecutor
    {
        SomethingResult Execute(CommandOperator  op);
    }
}