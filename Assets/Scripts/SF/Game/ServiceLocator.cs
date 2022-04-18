using SF.Common.Actors.Factories;
using SF.Common.Logger;
using SF.Common.Ticks;

namespace SF.Game
{
    public class ServiceLocator : IServiceLocator
    {
        public IDebugLogger Logger { get; }
        public ITickProcessor TickProcessor { get; }
        public IActorFactory ActorFactory { get; }
        
        public ServiceLocator()
        {
            Logger = new UnityDebugLogger();
            TickProcessor = new TickProcessor();
            ActorFactory = new ActorFactory();
        }
    }
}