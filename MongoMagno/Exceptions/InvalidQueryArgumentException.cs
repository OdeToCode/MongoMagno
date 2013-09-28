using System;

namespace MongoMagno.Exceptions
{
    public class InvalidQueryArgumentException : Exception
    {
        public InvalidQueryArgumentException(string message, Exception inner)
            :base(message,inner)
        {
            
        }
    }
}