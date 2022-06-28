using System;

namespace SF.Common.Commands
{
    public interface IObservableCommand : ICommand
    {
        event EventHandler OnComplete;
        event EventHandler OnFail;
    }
}