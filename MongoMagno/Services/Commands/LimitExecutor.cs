using System;
using MongoMagno.Exceptions;

namespace MongoMagno.Services.Commands
{
    public class LimitExecutor : IApplyOperation
    {
        public MongoDbResults Apply(CommandOperator op, MongoDbResults result)
        {
            var count = 0;
            if(op.Arguments.ElementCount > 0)
            try
            {
                count = op.Arguments[0].AsInt32;
            }
            catch (InvalidCastException ex)
            {
                var message = string.Format("Invalid argument for limit", ex);
                throw new InvalidQueryArgumentException(message, ex);
            }

            result.Cursor.Limit(count);
            return result;
        }
    }
}