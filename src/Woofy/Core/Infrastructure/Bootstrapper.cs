using System.Windows.Forms;
using Woofy.Settings;

namespace Woofy.Core.Infrastructure
{
	public static class Bootstrapper
	{
		public static void BootstrapApplication()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			ContainerAccesor.RegisterComponents();
			UserSettings.Initialize();
		}
	}
}