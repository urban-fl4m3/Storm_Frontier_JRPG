using SF.Common.Actors;
using SF.Common.Logger;
using SF.Common.Ticks;

namespace SF.Game
{
    public class ServiceLocator : IServiceLocator
    {
        public IDebugLogger Logger { get; }
        public ITickProcessor TickProcessor { get; }
        public IActorRegisterer ActorRegisterer { get; }

        public ServiceLocator()
        {
            Logger = new UnityDebugLogger();
            TickProcessor = new TickProcessor();
            ActorRegisterer = new ActorRegisterer();
        }
    }
}