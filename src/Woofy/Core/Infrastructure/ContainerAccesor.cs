using System;
using Autofac;
using Autofac.Builder;

namespace Woofy.Core.Infrastructure
{
	public static class ContainerAccesor
	{
		public static IContainer Container { get; private set; }

		public static void RegisterComponents()
		{
			if (Container != null)
				throw new InvalidOperationException("The container has already been initialized.");

			var builder = new ContainerBuilder();
			builder.SetDefaultScope(InstanceScope.Factory);

			builder.RegisterModule(new DefaultComponentsModule());
			builder.RegisterModule(new SingletonComponentsModule());

			Container = builder.Build();
		}
		
		public static T Resolve<T>()
		{
			return Container.Resolve<T>();
		}
	}
}