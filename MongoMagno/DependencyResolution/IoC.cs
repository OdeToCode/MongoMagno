using MongoMagno.Services;
using StructureMap;
namespace MongoMagno.DependencyResolution 
{
    public static class IoC 
    {
        public static IContainer Initialize()
        {
            var routeTable = new CommandRouteTable();
            routeTable.Initialize();

            ObjectFactory.Initialize(x =>
                {
                    x.For<IMongoDb>().Use<MongoDb>();
                    x.For<CommandRouteTable>().Use(routeTable);
                }
           );
            return ObjectFactory.Container;
        }
    }
}