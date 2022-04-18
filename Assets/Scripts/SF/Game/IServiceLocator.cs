using SF.Common.Actors.Factories;
using SF.Common.Logger;
using SF.Common.Ticks;

namespace SF.Game
{
    public interface IServiceLocator
    {
        IDebugLogger Logger { get; }
        ITickProcessor TickProcessor { get; }
        IActorFactory ActorFactory { get; }
    }
}