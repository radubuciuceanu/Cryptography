using System.Reactive.Linq;
using Caliburn.Micro;
using Cryptography.Business.Commands;
using Cryptography.Business.Commands.Contract;
using Cryptography.Business.Models.Inputs;

namespace Cryptography.Presentation.ViewModels
{
	public class CryptographyViewModel : PropertyChangedBase
	{
		private readonly IWindowManager windowManager;
		private readonly ICesarAlgorithm cesarAlgorithm;

		private string encrypted;
		private string decrypted;

		public string Encrypted
		{
			get => encrypted;
			set
			{
				encrypted = value;
				NotifyOfPropertyChange();
				NotifyOfPropertyChange(() => CanCesarDecrypt);
			}
		}

		public string Decrypted
		{
			get => decrypted;
			set
			{
				decrypted = value;
				NotifyOfPropertyChange();
				NotifyOfPropertyChange(() => CanCesarEncrypt);
			}
		}

		public bool CanCesarEncrypt => !string.IsNullOrWhiteSpace(Decrypted);

		public bool CanCesarDecrypt => !string.IsNullOrWhiteSpace(Encrypted);

		public CryptographyViewModel(IWindowManager windowManager)
		{
			this.windowManager = windowManager;
			cesarAlgorithm = new CesarAlgorithm();
		}

		public void CesarEncrypt()
		{
			var viewModel = new CesarViewModel();
			windowManager.ShowDialog(viewModel);
			Encrypted = cesarAlgorithm.Execute(new CesarInput {Key = viewModel.Key, Value = Decrypted}).Wait();
		}

		public void CesarDecrypt()
		{
			var viewModel = new CesarViewModel();
			windowManager.ShowDialog(viewModel);
			Decrypted = cesarAlgorithm.Execute(new CesarInput {Key = -viewModel.Key, Value = Encrypted}).Wait();
		}
	}
}