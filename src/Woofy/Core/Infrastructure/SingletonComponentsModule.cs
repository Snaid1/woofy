using Autofac.Builder;

namespace Woofy.Core.Infrastructure
{
	public class SingletonComponentsModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.Register<ComicStore>().As<IComicStore>().SingletonScoped();
			builder.Register<DefinitionStore>().As<IDefinitionStore>().SingletonScoped();
			builder.Register<BotSupervisor>().As<IBotSupervisor>().SingletonScoped();
			builder.Register(c => Program.SynchronizationContext);
		}
	}
}