using MongoMagno.Services;
using StructureMap;
namespace MongoMagno.DependencyResolution 
{
    public static class IoC 
    {
        public static IContainer Initialize() 
        {
            ObjectFactory.Initialize(x => x.For<IMongoDb>().Use<MongoDb>());
            return ObjectFactory.Container;
        }
    }
}