using MongoMagno.Services.Commands;
using MongoMagno.Services.Mongo;
using StructureMap;

namespace MongoMagno.Services.IoC 
{
    public static class IoC 
    {
        public static IContainer Initialize()
        {
            ObjectFactory.Initialize(x =>
                {                    
                    x.For<IMongoDb>().Use<MongoDb>();
                    x.For<IExecutorResolver>().Singleton().Use<ExecutorResolver>();
                    x.For<CommandRouteContainer>().Singleton().Use<CommandRouteContainer>();
                });
            return ObjectFactory.Container;
        }
    }
}