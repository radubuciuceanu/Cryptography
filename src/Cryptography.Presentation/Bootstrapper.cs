using System.Windows;
using Caliburn.Micro;
using Cryptography.Presentation.ViewModels;

namespace Cryptography.Presentation
{
	public class Bootstrapper : BootstrapperBase
	{
		public Bootstrapper()
		{
			Initialize();
		}

		protected override void OnStartup(object sender, StartupEventArgs e)
		{
			var windowManager = IoC.Get<IWindowManager>();
			windowManager.ShowWindow(new CryptographyViewModel());
		}
	}
}