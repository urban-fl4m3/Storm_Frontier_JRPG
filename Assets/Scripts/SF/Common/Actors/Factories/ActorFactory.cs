using SF.Game;
using UnityEngine;

namespace SF.Common.Actors.Factories
{
    public class ActorFactory
    {
        private readonly IServiceLocator _serviceLocator;

        public ActorFactory(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        public TActor Create<TActor>(TActor prefab) where TActor : Actor
        {
            var instance = Object.Instantiate(prefab);

            return instance;
        }
    }
}