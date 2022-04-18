using UnityEngine;

namespace SF.Common.Actors.Factories
{
    public class ActorFactory : IActorFactory
    {
        public TActor Create<TActor>(TActor prefab, ActorSpawnData spawnData) where TActor : Actor
        {
            var instance = Object.Instantiate(prefab, spawnData.Position, spawnData.Rotation, spawnData.Parent);
            return instance;
        }
    }
}