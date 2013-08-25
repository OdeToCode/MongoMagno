using System.Web.Http;
using System.Web.Mvc;
using MongoMagno.DependencyResolution;

[assembly: WebActivator.PreApplicationStartMethod(typeof(MongoMagno.App_Start.StructuremapMvc), "Start")]

namespace MongoMagno.App_Start 
{
	public static class StructuremapMvc 
	{
		public static void Start() 
		{
			var container = IoC.Initialize();
			DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
			GlobalConfiguration.Configuration.DependencyResolver = new StructureMapDependencyResolver(container);
		}
	}
}