using System;
using System.Linq;
using System.Reactive.Linq;
using Cryptography.Business.Commands.Contract;
using Cryptography.Business.Models.Inputs;

namespace Cryptography.Business.Commands
{
	public class CesarAlgorithm : ICesarAlgorithm
	{
		private const int StartIndex = 'A';
		private const int EndIndex = 'Z';

		public IObservable<string> Execute(CesarInput parameter)
		{
			return SplitIntoCharacters(parameter.Value)
				.Select(character => IncrementCharacter(character, parameter.Key))
				.Switch()
				.Buffer(int.MaxValue)
				.Select(characters => new string(characters.ToArray()));
		}

		private static IObservable<char> SplitIntoCharacters(string value)
		{
			return Observable
				.Return(value)
				.SelectMany(valueAsString => valueAsString.ToUpper().ToCharArray());
		}


		private static IObservable<char> IncrementCharacter(char character, int key)
		{
			return Observable
				.Return(character + key)
				.Select(integer => integer <= EndIndex ? integer : integer - EndIndex + StartIndex - 1)
				.Select(integer => integer >= StartIndex ? integer : StartIndex + EndIndex - integer - 1)
				.Select(integer => (char) integer);
		}
	}
}