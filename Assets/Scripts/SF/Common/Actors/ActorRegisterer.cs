using System;

namespace SF.Common.Actors
{
    public class ActorRegisterer : IActorRegisterer
    {
        public event EventHandler<IActor> ObjectRegistered;
        public event EventHandler<IActor> ObjectUnregistered;

        public ActorRegisterer()
        {
            
        }
        
        public void Add(IActor obj)
        {
            
        }

        public void Remove(IActor obj)
        {
            throw new NotImplementedException();
        }
    }
}