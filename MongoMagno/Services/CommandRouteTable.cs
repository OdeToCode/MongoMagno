using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MongoMagno.Services
{
    public class CommandRouteTable  
    {        
        public CommandRouteTable Initialize()
        {
            var executorTypes = Assembly.GetExecutingAssembly()
                                    .GetExportedTypes()
                                    .Where(type => type.Namespace == "MongoMagno.Services")
                                    .Where(type => typeof (CommandExecutor).IsAssignableFrom(type) && !type.IsAbstract);
                                                                      
            foreach (var executorType in executorTypes)
            {
                var attribute = executorType.GetCustomAttribute<CommandMatchAttribute>();
                if (attribute != null)
                {
                     _routeExpressions.Add(
                        executorType, 
                        new Regex(attribute.Pattern, _regexOptions)
                    );   
                }                
            }

            return this;
        }

        public RouteMatchResult Match(string command)
        {
            foreach (var pair in _routeExpressions)
            {
                var match = pair.Value.Match(command);                
                if (match.Success)
                {
                    var result = new RouteMatchResult();
                    result.Type = pair.Key;
                    foreach(var groupName in pair.Value.GetGroupNames())
                    {
                        if (match.Groups[groupName].Success)
                        {
                            result.Tokens.Add(groupName, match.Groups[groupName].Value);
                        }
                    }
                    return result;
                }
            }
            return RouteMatchResult.Empty;
        }

        private const RegexOptions _regexOptions = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline;
        private readonly Dictionary<Type, Regex> _routeExpressions = new Dictionary<Type, Regex>();
    }
}