using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using MongoMagno.Services.Routing;

namespace MongoMagno.Services.Commands
{
    public class CommandRouteContainer  
    {
        readonly IExecutorResolver _resolver;

        public CommandRouteContainer(IExecutorResolver resolver)
        {
            _resolver = resolver;
            var executorTypes = LoadAllExecutorTypes();
            RegisterExecutorTypes(executorTypes);
        }        

        public RouteMatchResult FindRouteForCommand(string command)
        {
            foreach (var pair in _routeExpressions)
            {
                var match = pair.Value.Match(command);                
                if (match.Success)
                {
                    return CreateRouteMatchResult(pair, match);
                }
            }

            return CreateDefaultExecutor();
        }

        private RouteMatchResult CreateDefaultExecutor()
        {
            return new RouteMatchResult()
                {
                    Executor = _resolver.GetInstance(typeof(InterpretiveExecutor))
                };
        }

        RouteMatchResult CreateRouteMatchResult(KeyValuePair<Type, Regex> pair, Match match)
        {
            var result = new RouteMatchResult();
            result.Executor = _resolver.GetInstance(pair.Key);
            foreach (var groupName in pair.Value.GetGroupNames())
            {
                if (match.Groups[groupName].Success)
                {
                    result.Tokens.Add(groupName, match.Groups[groupName].Value);
                }
            }
            return result;
        }

        void RegisterExecutorTypes(IEnumerable<Type> executorTypes)
        {
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
        }

        IEnumerable<Type> LoadAllExecutorTypes()
        {
            return Assembly.GetExecutingAssembly()
                           .GetExportedTypes()
                           .Where(type => type.Namespace == "MongoMagno.Services")
                           .Where(type => typeof(ICommandExecutor).IsAssignableFrom(type) && !type.IsAbstract);
        }

        private const RegexOptions _regexOptions = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline;
        private readonly Dictionary<Type, Regex> _routeExpressions = new Dictionary<Type, Regex>();
    }
}