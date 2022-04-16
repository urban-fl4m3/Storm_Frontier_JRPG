using SF.Common.Logger;

namespace SF.Game
{
    public interface IServiceLocator
    {
        IDebugLogger Logger { get; }
    }
}