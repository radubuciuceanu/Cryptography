using Cryptography.Business.Models.Inputs;

namespace Cryptography.Business.Commands.Contract
{
    public interface ICesarAlgorithm : ICommand<CesarInput, string>
    {
    }
}