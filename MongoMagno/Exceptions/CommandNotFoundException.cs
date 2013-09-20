using System;

namespace MongoMagno.Exceptions
{
    public class CommandNotFoundException : Exception
    {
        public CommandNotFoundException(string name)
            :base(String.Format("The command '{0}' is unrecognized", name))
        {
            
        }    
    }
}