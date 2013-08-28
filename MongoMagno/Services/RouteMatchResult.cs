using System;

namespace MongoMagno.Services
{
    public class RouteMatchResult
    {
        public RouteMatchResult()
        {
            Tokens = new RouteMatchTokens();
        }

        public Type Type { get; set; }
        public RouteMatchTokens Tokens { get; set; }
        public static RouteMatchResult Default = new RouteMatchResult() {Type = typeof (InterpretiveExecutor)};
    }
}