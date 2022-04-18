using SF.Common.Actors;
using SF.Common.Logger;
using SF.Common.Ticks;

namespace SF.Game
{
    public interface IServiceLocator
    {
        IDebugLogger Logger { get; }
        ITickProcessor TickProcessor { get; }
        IActorRegisterer ActorRegisterer { get; }
    }
}