using System;

namespace MongoMagno.Exceptions
{
    public class ExecutionException : Exception
    {
        public ExecutionException(Exception inner)
            :base("Syntax error in the command", inner)
        {
            
        } 
    }
}