using System;

namespace MongoMagno.Services
{
    public class CommandMatchAttribute : Attribute
    {
        public string Pattern { get; set; }

        public CommandMatchAttribute(string pattern)
        {
            Pattern = pattern;
        }
    }
}