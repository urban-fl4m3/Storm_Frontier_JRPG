using SF.Common.Factories;
using UnityEngine;

namespace SF.Common.Actors.Factories
{
    public class SceneActorFactory : IFactory
    {
        public TActor Create<TActor>(TActor prefab, ActorSpawnData spawnData) where TActor : SceneActor
        {
            var instance = Object.Instantiate(prefab, spawnData.Position, spawnData.Rotation, spawnData.Parent);
            return instance;
        }
    }
}