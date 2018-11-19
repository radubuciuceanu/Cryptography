using System.Reactive.Linq;
using Cryptography.Business.Commands;
using Cryptography.Business.Commands.Contract;
using Cryptography.Business.Models.Inputs;
using Moq;
using Shouldly;
using Xunit;

namespace Cryptography.Business.UnitTests.Commands
{
    public class CesarAlgorithmTests
    {
        private readonly ICesarAlgorithm _instance;
        private readonly CesarInput _decryptedInput;
        private readonly CesarInput _encryptedInput;
        private readonly string _something;
        private readonly Mock<IDummyRepository> _repository;

        public CesarAlgorithmTests()
        {
            _decryptedInput = new CesarInput {Key = 1};
            _encryptedInput = new CesarInput {Key = -1};
            _something = "Random something here.";
            _repository = BuildRepository();
            _instance = new CesarAlgorithm(_repository.Object);
        }
        
        [Fact]
        public void Execute_ReturnsResult_ReturnedByRepository()
        {
            string actual = _instance.Execute(_decryptedInput).Wait();
            
            actual.ShouldBe(_something);
            
            _repository.Verify(instance => instance.GetSomething(), Times.Exactly(5));
        }

        [Fact]
        public void Execute_IncrementsEachCharacter_WhenIncrementationDoesNotExceed()
        {
            _decryptedInput.Value = "a";

            string actual = _instance.Execute(_decryptedInput).Wait();

            actual.ShouldBe("B");
        }

        [Fact]
        public void Execute_IncrementsEachCharacter_WhenIncrementationExceeds()
        {
            _decryptedInput.Value = "z";

            string actual = _instance.Execute(_decryptedInput).Wait();

            actual.ShouldBe("A");
        }

        [Fact]
        public void Execute_DecrementEachCharacter_WhenDecrementationIsDoesNotExceed()
        {
            _encryptedInput.Value = "b";

            string actual = _instance.Execute(_encryptedInput).Wait();

            actual.ShouldBe("A");
        }

        [Fact]
        public void Execute_DecrementEachCharacter_WhenDecrementationIsExceeds()
        {
            _encryptedInput.Value = "a";

            string actual = _instance.Execute(_encryptedInput).Wait();

            actual.ShouldBe("Z");
        }

        private Mock<IDummyRepository> BuildRepository()
        {
            var mock = new Mock<IDummyRepository>();

            mock
                .Setup(instance => instance.GetSomething())
                .Returns(Observable.Return(_something));

            return mock;
        }
    }
}