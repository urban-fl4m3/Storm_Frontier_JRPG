using System;
using System.Collections.Generic;
using SF.Common.Logger;
using SF.Common.Registerers;

namespace SF.Common.Actors
{
    public abstract class ActorRegisterer<TActor> : IRegisterer<TActor> where TActor : class, IActor
    {
        public event EventHandler<TActor> ObjectRegistered;
        public event EventHandler<TActor> ObjectUnregistered;

        protected IDebugLogger Logger { get; }
        protected HashSet<TActor> RegisteredActors { get; }

        protected ActorRegisterer(IDebugLogger logger)
        {
            Logger = logger;
            RegisteredActors = new HashSet<TActor>();
        }

        public bool Add(TActor obj)
        {
            if (RegisteredActors.Contains(obj))
            {
                Logger.LogWarning($"Actor {obj} is already registered");
                return false;
            }

            RegisteredActors.Add(obj);
            ObjectRegistered?.Invoke(this, obj);

            return true;
        }

        public bool Remove(TActor obj)
        {
            if (!RegisteredActors.Contains(obj))
            {
                Logger.LogWarning($"Actor {obj} wasn't registered");
                return false;
            }

            RegisteredActors.Remove(obj);
            ObjectUnregistered?.Invoke(this, obj);

            return true;
        }

        public IEnumerable<TActor> GetAll()
        {
            return RegisteredActors;
        }
    }
}