namespace MongoMagno.Services.Commands
{
    public class LimitExecutor : IApplyOperation
    {
        public MongoDbResults Apply(CommandOperator op, MongoDbResults result)
        {

            //result.Cursor.Limit(op.Arguments);
            return result;
        }
    }
}