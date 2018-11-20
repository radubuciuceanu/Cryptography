using Caliburn.Micro;

namespace Cryptography.Presentation.ViewModels
{
	public class CryptographyViewModel : PropertyChangedBase
	{
		public string Encrypted { get; set; }

		public string Decrypted { get; set; }

		public void CesarEncrypt()
		{
		}

		public void CesarDecrypt()
		{
		}
	}
}