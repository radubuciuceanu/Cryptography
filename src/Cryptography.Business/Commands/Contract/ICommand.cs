using System;

namespace Cryptography.Business.Commands.Contract
{
    public interface ICommand<in TInput, out TOutput>
    {
        IObservable<TOutput> Execute(TInput parameter);
    }
}