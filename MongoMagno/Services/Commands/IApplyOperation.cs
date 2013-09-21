using MongoMagno.Models;

namespace MongoMagno.Services.Commands
{
    public interface IApplyOperation
    {
        MongoDbResults Apply(CommandOperator  op, MongoDbResults results);
    }
}