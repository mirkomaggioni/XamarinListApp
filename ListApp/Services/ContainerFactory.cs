using Autofac;
using ListApp.BusinessObjects;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace ListApp.Services
{
	public static class ContainerFactory
	{
		private static readonly ContainerBuilder containerBuilder = new ContainerBuilder();
		private static IContainer container;

		public static void Init()
		{
			var serializer = new XmlSerializer(typeof(AppSettings));
			var assembly = IntrospectionExtensions.GetTypeInfo(typeof(AuthenticationService)).Assembly;
			var stream = assembly.GetManifestResourceStream("ListApp.AppSettings.xml");

			using (var reader = new StreamReader(stream))
			{
				var settings = (AppSettings)serializer.Deserialize(reader);
				containerBuilder.Register(_ => new AuthenticationService(settings.ServerAppUrl));
			}

			container = containerBuilder.Build();
		}

		public static TService Get<TService>() where TService : class
		{
			return container.Resolve<TService>();
		}
	}
}