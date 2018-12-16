using Ecommittees.DataContext;
using Ecommittees.Services;
using Owin;
using System.Composition.Hosting;
using WinInsider.HttpHosting.Composition;
using WinInsider.HttpHosting.Http;
using WinInsider.HttpHosting.Owin;
using Newtonsoft.Json;

namespace Universal.TrayHost
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			var configuration = new ContainerConfiguration();
			configuration.WithExports(typeof(MembersService).Assembly);
			configuration.WithAssembly(typeof(IDataContext).Assembly);

			var container = configuration.CreateContainer();

            var config = new HttpConfiguration();
            config.RouteTemplate = "api/{service}/{operation}";
            config.DependencyResolver = new StandaloneDependencyResolver(container);
			config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;


			app.UseRouteTemplate(config);
		}
	}
}
